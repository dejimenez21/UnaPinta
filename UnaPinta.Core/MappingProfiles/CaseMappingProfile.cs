using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Enums;
using UnaPinta.Dto.Models;
using UnaPinta.Dto.Models.Case;
using UnaPinta.Dto.Models.Donor;

namespace UnaPinta.Core.MappingProfiles
{
    public class CaseMappingProfile : Profile
    {
        public CaseMappingProfile()
        {
            #region To Entity
            CreateMap<CreateCaseDto, Case>();
            #endregion

            #region To Dto
            CreateMap<Case, CaseDetailsDto>().ConvertUsing((entity, dto, cfg) =>
            {
                if (dto == null) dto = new CaseDetailsDto();
                dto.Id = entity.Id;
                dto.CreatedAt = entity.CreatedAt;

                dto.Status = entity.StatusNav.Description;
                dto.Request = cfg.Mapper.Map<RequestDetailsDto>(entity.RequestNav);
                dto.Donor = cfg.Mapper.Map<DonorInfoDto>(entity.DonorNav);

                return dto;
            });
            CreateMap<Case, CaseForRequestDto>().ConvertUsing((entity, dto, cfg) =>
            {
                if (dto == null) dto = new CaseForRequestDto();
                dto.Id = entity.Id;
                dto.CreatedAt = entity.CreatedAt;
                dto.Status = entity.StatusNav.Description;
                dto.Donor = cfg.Mapper.Map<DonorInfoDto>(entity.DonorNav);

                return dto;
            });
            #endregion
        }
    }
}
