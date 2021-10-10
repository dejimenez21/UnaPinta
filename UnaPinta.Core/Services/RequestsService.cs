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
using UnaPinta.Dto.Models.Request;
using UnaPinta.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using UnaPinta.Core.Extensions;

namespace UnaPinta.Core.Services
{
    public class RequestsService : IRequestsService
    {
        private readonly IUnaPintaRepository _repo;
        private readonly UserManager<User> _userManager;
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        private readonly IRequestNotificationService _requestNotificationService;
        private readonly IProvinceService _provinceService;

        public RequestsService(IUnaPintaRepository repo, UserManager<User> userManager, 
            IRequestRepository requestRepository, IMapper mapper, IRequestNotificationService requestNotificationService,
            IProvinceService provinceService)
        {
            _repo = repo;
            _userManager = userManager;
            _requestRepository = requestRepository;
            _mapper = mapper;
            _requestNotificationService = requestNotificationService;
            _provinceService = provinceService;
        }

        public async Task<Func<Task>> CreateRequest(RequestCreateDto inputRequest, string userName)
        {
            var stringDate = await _requestRepository.SelectStringDateById((int)inputRequest.ResponseDueDateId);
            if (stringDate == null) throw new BaseDomainException("El intervalo de fecha especificado no existe.", 400);
            
            var province = await _provinceService.RetrieveProvinceByCode(inputRequest.ProvinceCode);
            if (province == null) throw new BaseDomainException("La provincia especificada no existe.", 400);

            var user = await _userManager.FindByNameAsync(userName); //TODO: Add validation if the user returns null

            if (inputRequest.ForMe)
                inputRequest = CompleteRequestForCurrentUser(inputRequest, user);
            else if (string.IsNullOrEmpty(inputRequest.Name) || !inputRequest.BirthDate.HasValue || inputRequest.BloodTypeId < 1)
                throw new BaseDomainException("El modelo es invalido", 400);

            var request = _mapper.Map<Request>(inputRequest);
            request.ResponseDueDate = stringDate.ToDateTime();
            request.ProvinceId = province.Id;
            request.Prescription = await inputRequest.PrescriptionImage.ToFileModel();
            request.RequesterId = user.Id;

            _repo.CreateRequest(request);
            await _repo.SaveChangesAsync();

            return async () => await _requestNotificationService.SendRequestNotification(request);
        }

        private RequestCreateDto CompleteRequestForCurrentUser(RequestCreateDto inputRequest, User user)
        {
            inputRequest.Name = $"{user.FirstName} {user.LastName}";
            inputRequest.BirthDate = user.BirthDate;
            inputRequest.BloodTypeId = (int)user.BloodTypeId;

            return inputRequest;
        }

        public async Task<RequestDetailsDto> RetrieveRequestDetailsById(int id)
        {
            var request = await _requestRepository.SelectRequestById(id);
            if (request == null) throw new RequestNotFoundException(id);

            var details = _mapper.Map<RequestDetailsDto>(request);

            return details;
        }

        public async Task<IEnumerable<RequestSummaryDto>> RetrieveRequestsSummaryByDonor(string username)
        {
            var donor = await _userManager.FindByNameAsync(username);
            var requests = await _requestRepository.SelectRequestsByDonor(donor);

            var requestsSummary = _mapper.Map<IEnumerable<RequestSummaryDto>>(requests);

            return requestsSummary;
        }

        public async Task<IEnumerable<StringDate>> RetrieveAllStringDates() =>
            await _requestRepository.SelectAllStringDates();

    }
}