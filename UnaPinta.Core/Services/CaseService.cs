using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts.Case;
using UnaPinta.Core.Exceptions;
using UnaPinta.Data.Contracts;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Enums;
using UnaPinta.Dto.Models.Case;

namespace UnaPinta.Core.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRequestRepository _requestRepository;

        public CaseService(ICaseRepository caseRepository, IMapper mapper, UserManager<User> userManager, IRequestRepository requestRepository)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
            _userManager = userManager;
            _requestRepository = requestRepository;
        }

        public async Task<CaseDetailsDto> CreateCase(CreateCaseDto inputCase, string userName)
        {
            //TODO: Validar que el donante no tenga un caso en proceso
            #region Validations
            var donor = await _userManager.FindByNameAsync(userName);
            if(donor == null)
            {
                //TODO: Add new custom exception for this case
                throw new BaseDomainException($"El usuario {userName} no existe.", 400);
            }
            if(!await _requestRepository.ExistsAsync(r => r.Id == inputCase.RequestId))
            {
                //TODO: Add new custom exception for this case
                throw new BaseDomainException($"El request especificado no existe.", 400);
            }
            #endregion
            //TODO: Validar que request no este lleno
            var caseEntity = _mapper.Map<Case>(inputCase);
            caseEntity.DonorId = donor.Id;
            caseEntity.StatusId = CaseStatusEnum.En_Proceso;

            _caseRepository.Insert(caseEntity);
            await _caseRepository.SaveChangesAsync();

            return await RetrieveCaseDetails(caseEntity.Id);
        }

        public async Task<CaseDetailsDto> RetrieveCaseDetails(long id)
        {
            var caseEntity = await _caseRepository.SelectOneAsync(c => c.Id == id, 
                e => e.Include(p => p.RequestNav).ThenInclude(r => r.Prescription)
                .Include(p => p.RequestNav).ThenInclude(r => r.BloodTypeNav)
                .Include(p => p.RequestNav).ThenInclude(r => r.BloodComponentNav)
                .Include(p => p.RequestNav).ThenInclude(r => r.RequesterNav)
                .Include(p => p.DonorNav).ThenInclude(u => u.BloodTypeNav)
                .Include(p => p.StatusNav));

            if(caseEntity == null)
            {
                //TODO: Add new custom exception for this case
                throw new BaseDomainException($"El caso especificado no existe.", 404);
            }

            var caseDetails = _mapper.Map<CaseDetailsDto>(caseEntity);
            return caseDetails;
        }

        public async Task<CaseForRequestDto> MarkCaseAsCompleted(long id, string requesterUsername)
        {
            var caseEntity = await _caseRepository.SelectByIdAsync(id);
            //TODO: Create custom exception
            if (caseEntity == null || caseEntity.DeletedAt.HasValue) throw new BaseDomainException($"No existe un caso con id {id}", 404);

            var requester = await _userManager.FindByNameAsync(requesterUsername);
            if(caseEntity.RequestNav.RequesterId != requester.Id) throw new BaseDomainException($"No tiene permisos para modificar el estado de este caso", 403);

            caseEntity.StatusId = CaseStatusEnum.Completado;
            await _caseRepository.SaveChangesAsync();

            var completedCase = _mapper.Map<CaseForRequestDto>(caseEntity);
            return completedCase;
        }
    }
}
