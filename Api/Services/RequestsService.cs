using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Entities;
using Api.Helpers;
using Api.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Api.Services
{
    public class RequestsService : IRequestsService
    {
        private readonly IUnaPintaRepository _repo;
        private readonly UserManager<User> _userManager;

        public RequestsService(IUnaPintaRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task CreateRequest(Request request, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            request.RequesterId = user.Id;

            _repo.CreateRequest(request);
            await _repo.SaveChangesAsync();
        }


        public async Task SendRequestNotification(Request request)
        {  
            var requester = await _repo.GetUserById(request.RequesterId);
            var compatibleUsers = await GetCompatibleUsers(requester.BloodTypeId);
            var CompleteRequest = await _repo.GetRequestById(request.Id);
            foreach (var user in compatibleUsers)
            {
                if(!(await IsAvailable(user)))
                    continue;
                EmailSender sender = new EmailSender(_repo);
                await sender.SendNotification(user, CompleteRequest);
                await sender.Disconnect();
            }
        }

        private Task<IEnumerable<User>> GetCompatibleUsers(BloodTypeEnum bloodTypeEnum)
        {
            var dict = new BloodTypeDictionary();
            var CompatibleBloodTypes = dict.GetCompatibleWith(bloodTypeEnum);
            return _repo.GetDonorsByBloodType(CompatibleBloodTypes);
        }

        private async Task<bool> IsAvailable(User donor)
        {
            if(!donor.CanDonate)
                return false;

            var availableAt = await _repo.GetAvailabilityDateByDonorId(donor.Id);

            if(availableAt>DateTime.Now)
                return false;

            return true;
        }
    }
}