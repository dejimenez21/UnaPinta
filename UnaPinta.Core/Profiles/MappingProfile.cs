﻿using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Dto.Enums;
using UnaPinta.Dto.Models.Request;

namespace UnaPinta.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //From DTO to Entity
            CreateMap<RequestCreateDto, Request>();
            CreateMap<int, RequestPossibleBloodTypes>()
                .ForMember(rpb => rpb.BloodTypeId, opt => opt.MapFrom(i => (BloodTypeEnum)i));
            CreateMap<Register, User>();
            CreateMap<Register, Role>();
            CreateMap<WaitListItemCreate, WaitList>();
            CreateMap<UserSignUp, User>();

            //From Entity to DTO
            CreateMap<Role, RoleCreateResponseDto>();
            CreateMap<Request, RequestDetailsDto>()
                .ForMember(d => d.PatientName, opt => opt.MapFrom(x => x.Name))
                .ForMember(d => d.PatientPhone, opt => opt.MapFrom(x => x.RequesterNav.PhoneNumber))
                .ForMember(d => d.Prescription, opt => opt.MapFrom(x => x.PrescriptionBase64))
                .ForMember(d => d.BloodComponent, opt => opt.MapFrom(x => x.BloodComponentNav.Description))
                .ForMember(d => d.BloodType, opt => opt.MapFrom(x => x.BloodTypeNav.Description))
                .ForMember(d => d.RequesterEmail, opt => opt.MapFrom(x => x.RequesterNav.Email))
                .ForMember(d => d.RequesterName, opt => opt.MapFrom(x => x.RequesterNav.FirstName + " " + x.RequesterNav.LastName))
                .ForMember(d => d.RequesterPhone, opt => opt.MapFrom(x => x.RequesterNav.PhoneNumber));
            CreateMap<Request, RequestSummaryDto>()
                .ForMember(rs => rs.Province, opt => opt.MapFrom(r => r.ProvinceNav.Name));
        }
    }
    
}
