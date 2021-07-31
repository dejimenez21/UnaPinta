using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using System.Linq;
using UnaPinta.Data.Contracts;
using UnaPinta.Core.Contracts;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Core.Services
{
    public class WaitListServices : IWaitListServices
    {
        private readonly IUnaPintaRepository _repo;

        public WaitListServices(IUnaPintaRepository repo)
        {
            _repo = repo;
        }

        public async Task<DateTime> CalculateAvailableAtDate(WaitList item, int months)
        {
            var condition = await _repo.GetConditionById(item.ConditionId);
            var diff = condition.MonthsToWait - months;
            return DateTime.Now.AddMonths(diff);
        }

        public async Task ReviewDonorAvailability(int userId, List<WaitList> waitList)
        {
            var User = await _repo.GetUserById(userId);

            if(!waitList.Any())
            {
                await SendAbleToDonateNotification(User);
                return;
            }

            var userAge = (DateTime.Now - User.BirthDate).TotalDays/365;
            
            if(waitList.Any(x=>x.ConditionId==ConditionEnum.Inaceptable) || User.Weight<50 || userAge<18 || userAge>65)
            {
                User.CanDonate = false;
                await _repo.SaveChangesAsync();
                await SendUnableToDonateNotification(User);
                return;
            }

            var availableAt = waitList.Max(x=>x.AvailableAt);
            await SendAvailabilityDateNotification(User, availableAt);

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