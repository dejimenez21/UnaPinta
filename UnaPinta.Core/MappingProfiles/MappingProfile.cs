using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Dto.Enums;
using UnaPinta.Dto.Models.Request;
using UnaPinta.Core.Extensions;
using Microsoft.AspNetCore.Http;

namespace UnaPinta.Core.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //From DTO to Entity
            CreateMap<WaitListItemCreate, WaitList>();

            //From Entity to DTO
            CreateMap<File, byte[]>().ConvertUsing((entity, dto) => entity.FileContent);
            CreateMap<Role, RoleCreateResponseDto>();
            
        }
    }
    
}
