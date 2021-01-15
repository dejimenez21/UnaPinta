using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Entities;
using Api.Helpers;

namespace Api.Services
{
    public class RequestsService : IRequestsService
    {
        private readonly IUnaPintaRepository _repo;

        public RequestsService(IUnaPintaRepository repo)
        {
            _repo = repo;
        }

        public async Task SendRequestNotification(Request request)
        {
            var requester = await _repo.GetUserById(request.RequesterId);
            var compatibleUsers = await GetCompatibleUsers(requester.BloodTypeId);
            var CompleteRequest = await _repo.GetRequestById(request.Id);
            foreach (var user in compatibleUsers)
            {
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
    }
}