using UnaPinta.Data.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Dto.Models;

namespace UnaPinta.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //From DTO to Entity
            CreateMap<RequestCreate, Request>();
            CreateMap<Register, User>();
            CreateMap<Register, Role>();
            CreateMap<WaitListItemCreate, WaitList>();
            CreateMap<UserSignUp, User>();

            //From Entity to DTO

        }
    }
    
}
