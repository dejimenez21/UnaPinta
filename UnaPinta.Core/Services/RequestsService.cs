using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Helpers;
using UnaPinta.Data.Contracts;
using UnaPinta.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using UnaPinta.Dto.Enums;
using UnaPinta.Dto.Models;
using AutoMapper;
using UnaPinta.Core.Exceptions.Request;

namespace UnaPinta.Core.Services
{
    public class RequestsService : IRequestsService
    {
        private readonly IUnaPintaRepository _repo;
        private readonly UserManager<User> _userManager;
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        private readonly EmailSender _emailService;

        public RequestsService(IUnaPintaRepository repo, UserManager<User> userManager, 
            IRequestRepository requestRepository, IMapper mapper, EmailSender emailService)
        {
            _repo = repo;
            _userManager = userManager;
            _requestRepository = requestRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Func<Task>> CreateRequest(RequestCreate inputRequest, string userName)
        {
            var request = _mapper.Map<Request>(inputRequest);

            var user = await _userManager.FindByNameAsync(userName);
            request.RequesterId = user.Id;

            _repo.CreateRequest(request);
            await _repo.SaveChangesAsync();

            return async () => await this.SendRequestNotification(request);
        }

        public async Task<RequestDetailsDto> RetrieveRequestDetailsById(int id)
        {
            var request = await _requestRepository.SelectRequestById(id);
            if (request == null) throw new RequestNotFoundException(id);

            var details = _mapper.Map<RequestDetailsDto>(request);

            return details;
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

                await _emailService.SendNotification(user, CompleteRequest);
                await _emailService.Disconnect();
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