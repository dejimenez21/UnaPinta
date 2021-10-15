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
        private readonly ICaseRepository _caseRepository;
        private readonly IFileRepository _fileRepository;

        public RequestsService(IUnaPintaRepository repo, UserManager<User> userManager, 
            IRequestRepository requestRepository, IMapper mapper, IRequestNotificationService requestNotificationService,
            IProvinceService provinceService, ICaseRepository caseRepository, IFileRepository fileRepository)
        {
            _repo = repo;
            _userManager = userManager;
            _requestRepository = requestRepository;
            _mapper = mapper;
            _requestNotificationService = requestNotificationService;
            _provinceService = provinceService;
            _caseRepository = caseRepository;
            _fileRepository = fileRepository;
        }

        public async Task<Func<Task>> CreateRequest(RequestCreateDto inputRequest, string userName)
        {
            var stringDate = await _requestRepository.SelectStringDateById((int)inputRequest.ResponseDueDateId);
            if (stringDate == null) throw new BaseDomainException("El intervalo de fecha especificado no existe.", 400);
            
            var province = await _provinceService.RetrieveProvinceByCode(inputRequest.ProvinceCode);
            if (province == null) throw new BaseDomainException("La provincia especificada no existe.", 400);

            var request = _mapper.Map<Request>(inputRequest);
            request.ResponseDueDate = stringDate.ToDateTime();
            request.ProvinceId = province.Id;
            request.Prescription = await inputRequest.PrescriptionImage.ToFileModel();

            var user = await _userManager.FindByNameAsync(userName);
            request.RequesterId = user.Id;

            _requestRepository.Insert(request);
            await _requestRepository.SaveChangesAsync();

            return async () => await _requestNotificationService.SendRequestNotification(request);
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