using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using System.Linq;
using UnaPinta.Data.Contracts;
using UnaPinta.Core.Contracts;
using UnaPinta.Dto.Enums;
using UnaPinta.Data.Brokers.DateTimes;

namespace UnaPinta.Core.Services
{
    public class WaitListServices : IWaitListServices
    {
        private readonly IUnaPintaRepository _repo;
        private readonly IWaitListRepository _waitListRepository;
        private readonly IDateTimeBroker _dateTimeBroker;

        public WaitListServices(IUnaPintaRepository repo, IWaitListRepository waitListRepository, IDateTimeBroker dateTimeBroker)
        {
            _repo = repo;
            _waitListRepository = waitListRepository;
            _dateTimeBroker = dateTimeBroker;
        }

        public async Task<DateTime> CalculateAvailableAtDate(ConditionEnum conditionId, int months)
        {
            var condition = await _repo.GetConditionById(conditionId);
            var diff = condition.MonthsToWait - months;
            return _dateTimeBroker.GetCurrentDateTime().AddMonths(diff);
        }

        public async Task ReviewDonorAvailability(int userId, List<WaitList> waitList)
        {
            var User = await _repo.GetUserById(userId);

            if(!waitList.Any())
            {
                await SendAbleToDonateNotification(User);
                return;
            }

            var userAge = (_dateTimeBroker.GetCurrentDateTime() - User.BirthDate).TotalDays/365;

            User.CanDonate = true;
            if (waitList.Any(x=>x.ConditionId==ConditionEnum.Inaceptable) || User.Weight<50 || userAge<18 || userAge>65)
            {
                User.CanDonate = false;
                await SendUnableToDonateNotification(User);
                return;
            }
            await _repo.SaveChangesAsync();

            var availableAt = waitList.Max(x=>x.AvailableAt);
            await SendAvailabilityDateNotification(User, availableAt);

        }

        public async Task<bool> IsDonorAvailable(User donor)
        {
            if (!donor.CanDonate)
                return false;

            var items = await _waitListRepository.SelectWaitListItemsByDonorId(donor.Id);

            if (!items.Any(x => x.ConditionId != ConditionEnum.SinCondicion)) return true;

            var availableAt = items.Max(x => x.AvailableAt);

            return !(availableAt > _dateTimeBroker.GetCurrentDateTime());

        }

        async Task SendAvailabilityDateNotification(User user, DateTime availableAt)
        {
            return;
        }

        async Task SendUnableToDonateNotification(User user)
        {
            return;
        }

        async Task SendAbleToDonateNotification(User user)
        {
            return;
        }

        
    }
}