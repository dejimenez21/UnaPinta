using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using UnaPinta.Dto.Models.Donor;
using UnaPinta.Dto.Models.User;

namespace UnaPinta.Core.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            #region To Entity
            CreateMap<UserSignUp, User>();
            #endregion

            #region To Dto
            CreateMap<User, DonorInfoDto>().ConvertUsing((entity, dto) =>
            {
                if (dto == null) dto = new DonorInfoDto();
                dto.FullName = $"{entity.FirstName} {entity.LastName}";
                dto.BloodType = entity.BloodTypeNav.Description;
                dto.PhoneNumber = entity.PhoneNumber;
                dto.Email = entity.Email;

                return dto;
            });
            CreateMap<User, UserProfileDto>()
                .ForMember(up => up.BloodType, opt => opt.MapFrom(u => u.BloodTypeNav.Description))
                .ForMember(up => up.ProvinceCode, opt => opt.MapFrom(u => u.ProvinceNav.Code));
            #endregion
        }
    }
}
