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
using UnaPinta.Dto.Enumerations;
using UnaPinta.Data.Brokers.DateTimes;
using UnaPinta.Data.Brokers.Loggings;
using UnaPinta.Core.Exceptions.User;
using UnaPinta.Core.Exceptions.Province;
using UnaPinta.Core.Exceptions.BloodType;

namespace UnaPinta.Core.Services
{
    public class RequestsService : IRequestsService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        private readonly IProvinceService _provinceService;
        private readonly ICaseRepository _caseRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IDateTimeBroker _dateTimeBroker;
        private readonly ILoggingBroker _loggingBroker;

        public RequestsService(UserManager<User> userManager, IRequestRepository requestRepository, IMapper mapper, IProvinceService provinceService, 
            ICaseRepository caseRepository, IFileRepository fileRepository, IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker)
        {
            _userManager = userManager;
            _requestRepository = requestRepository;
            _mapper = mapper;
            _provinceService = provinceService;
            _caseRepository = caseRepository;
            _fileRepository = fileRepository;
            _dateTimeBroker = dateTimeBroker;
            _loggingBroker = loggingBroker;
        }

        public async Task<Request> CreateRequest(RequestCreateDto inputRequest, string userName)
        {
            int stringDateId = (int)inputRequest.ResponseDueDateId;
            var stringDate = await _requestRepository.SelectStringDateById(stringDateId);
            if (stringDate == null)
            {
                var ex = new StringDateNotFoundException(stringDateId);
                _loggingBroker.LogError(ex);
                throw ex;
            }
            
            var province = await _provinceService.RetrieveProvinceByCode(inputRequest.ProvinceCode);
            if (province == null)
            {
                var ex = new ProvinceNotFoundException(inputRequest.ProvinceCode);
                _loggingBroker.LogError(ex);
                throw ex;
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                var ex = new UserNotFoundException(userName, true);
                _loggingBroker.LogError(ex);
                throw ex;
            }

            if (inputRequest.ForMe)
                inputRequest = CompleteRequestForCurrentUser(inputRequest, user);
            else if (string.IsNullOrEmpty(inputRequest.Name) || !inputRequest.BirthDate.HasValue || !inputRequest.BloodTypeId.HasValue)
            {
                var ex = new PatientDataMissingException(string.IsNullOrEmpty(inputRequest.Name), !inputRequest.BirthDate.HasValue);
                _loggingBroker.LogError(ex);
                throw ex;
            }

            var patientBloodTypeEnum = (BloodTypeEnumeration)inputRequest.BloodTypeId;
            var incompatibleBloodTypes = patientBloodTypeEnum
                .GetIncompatibleBloodTypesAsReceiverFromList(inputRequest.PossibleBloodTypes.Select(x => (BloodTypeEnumeration)x));

            if(incompatibleBloodTypes != null && incompatibleBloodTypes.Any())
            {
                var ex = new IncompatibleBloodTypesException(incompatibleBloodTypes, patientBloodTypeEnum);
                _loggingBroker.LogError(ex);
                throw ex;
            }

            var request = _mapper.Map<Request>(inputRequest);
            request.ResponseDueDate = stringDate.ToDateTime(_dateTimeBroker.GetCurrentDateTime());
            request.ProvinceId = province.Id;
            request.Prescription = await inputRequest.PrescriptionImage.ToFileModel();
            request.RequesterId = user.Id;
            request.CreatedAt = _dateTimeBroker.GetCurrentDateTime();
            request.LastUpdatedAt = _dateTimeBroker.GetCurrentDateTime();

            _requestRepository.Insert(request);
            await _requestRepository.SaveChangesAsync();

            return request;
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
            foreach(var request in requestsSummary)
            {
                request.Status = await GetRequestStatus(requests.First(r => r.Id == request.Id));
            }

            return requestsSummary;
        }

        public async Task<IEnumerable<StringDate>> RetrieveAllStringDates() =>
            await _requestRepository.SelectAllStringDates();

        public async Task<IEnumerable<RequestSummaryDto>> RetrieveRequestsSummaryByRequester(string username, string name = null)
        {
            //TODO: Validar que el usuario exista
            var requests = await _requestRepository.SelectRequestByRequester(username, name);
            var requestsSummary = _mapper.Map<IEnumerable<RequestSummaryDto>>(requests);
            foreach (var request in requestsSummary)
            {
                request.Status = await GetRequestStatus(requests.First(r => r.Id == request.Id));
            }
            return requestsSummary;
        }

        public async Task DeleteRequestById(long id, string ownerUserName)
        {
            var request = await _requestRepository.SelectByIdAsync(id);
            //TODO: Create custom exception
            if (request == null || request.DeletedAt.HasValue) throw new BaseDomainException($"La solicitud con el id {id} no existe", 404);

            var owner = await _userManager.FindByNameAsync(ownerUserName);
            if(owner == null || request.RequesterId != owner.Id) throw new BaseDomainException($"No tiene permisos para eliminar esta solicitud", 403);

            var cases = await _caseRepository.SelectCasesByRequestId(request.Id);
            request.Cases = cases.ToList();

            var requestStatus = await GetRequestStatus(request);
            //TODO: Create custom exception
            if(requestStatus != RequestStatusEnumeration.REGISTERED) throw new BaseDomainException($"La solicitud no puede ser eliminada", 400);

            _requestRepository.Delete(request);
            _caseRepository.DeleteRange(cases);
            var file = await _fileRepository.SelectById(request.PrescriptionImageId);
            _fileRepository.Delete(file);

            await _requestRepository.SaveChangesAsync();
        }

        public async Task<string> GetRequestStatus(Request request)
        {
            //TODO: Evaluar eficiencia
            long completedCasesAmt;

            if(request.Cases == null)
            {
                completedCasesAmt = await _caseRepository.CountAsync(c => c.RequestId == request.Id && c.StatusId == CaseStatusEnum.Completado);
            }
            else
            {
                completedCasesAmt = request.Cases.Count(c => c.StatusId == CaseStatusEnum.Completado);
            }
                
            if (completedCasesAmt == 0)
                return RequestStatusEnumeration.REGISTERED;
            else if (completedCasesAmt < request.Amount)
                return RequestStatusEnumeration.IN_PROCESS;
            else
                return RequestStatusEnumeration.COMPLETED;

        }

        public async Task<RequestCasesDto> RetrieveRequestWithCases(long id, string ownerUserName)
        {
            var request = await _requestRepository.SelectByIdAsync(id);
            //TODO: Create custom exception
            if (request == null || request.DeletedAt.HasValue) throw new BaseDomainException($"La solicitud con el id {id} no existe", 404);

            var owner = await _userManager.FindByNameAsync(ownerUserName);
            if (owner == null || request.RequesterId != owner.Id) throw new BaseDomainException($"No tiene permisos para eliminar esta solicitud", 403);

            var requestCases = _mapper.Map<RequestCasesDto>(request);
            requestCases.Status = await GetRequestStatus(request);

            return requestCases;
        }

        public async Task MarkRequestAsCompleted(long id, string ownerUserName)
        {
            var request = await _requestRepository.SelectByIdAsync(id);
            //TODO: Create custom exception
            if (request == null || request.DeletedAt.HasValue) throw new BaseDomainException($"La solicitud con el id {id} no existe", 404);

            var owner = await _userManager.FindByNameAsync(ownerUserName);
            if (owner == null || request.RequesterId != owner.Id) throw new BaseDomainException($"No tiene permisos para editar esta solicitud", 403);

            var completedCases = request.Cases.Where(c => !c.DeletedAt.HasValue && c.StatusId == CaseStatusEnum.Completado);

            if (!completedCases.Any())
                throw new BaseDomainException($"No se puede completar esta solicitud, ya que no tiene ningun caso finalizado", 403);

            request.Amount = completedCases.Count();

            foreach(var caseToCancel in request.Cases.Where(c => !c.DeletedAt.HasValue && c.StatusId == CaseStatusEnum.En_Proceso))
            {
                caseToCancel.StatusId = CaseStatusEnum.Cancelado;
            }

            await _requestRepository.SaveChangesAsync();
        }

    }
}