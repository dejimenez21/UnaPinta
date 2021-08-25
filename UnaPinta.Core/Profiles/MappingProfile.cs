using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Dto.Models;
using UnaPinta.Dto.Enums;

namespace UnaPinta.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //From DTO to Entity
            CreateMap<RequestCreate, Request>();
            CreateMap<int, RequestPossibleBloodTypes>()
                .ForMember(rpb => rpb.BloodTypeId, opt => opt.MapFrom(i => (BloodTypeEnum)i));
            CreateMap<Register, User>();
            CreateMap<Register, Role>();
            CreateMap<WaitListItemCreate, WaitList>();
            CreateMap<UserSignUp, User>();

            //From Entity to DTO

        }
    }
    
}
