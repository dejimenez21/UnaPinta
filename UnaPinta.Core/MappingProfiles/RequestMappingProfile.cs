using AutoMapper;
using UnaPinta.Dto.Models.Request;
using UnaPinta.Dto.Models;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Enums;
using UnaPinta.Core.Extensions;

namespace UnaPinta.Core.MappingProfiles
{
    public class RequestMappingProfile : Profile
    {
        public RequestMappingProfile()
        {
            #region To Entity
            CreateMap<RequestCreateDto, Request>();
            CreateMap<int, RequestPossibleBloodTypes>()
                .ForMember(rpb => rpb.BloodTypeId, opt => opt.MapFrom(i => (BloodTypeEnum)i));
            #endregion

            #region To Dto
            CreateMap<Request, RequestDetailsDto>()
                .ForMember(d => d.PatientName, opt => opt.MapFrom(x => x.Name))
                .ForMember(d => d.PatientPhone, opt => opt.MapFrom(x => x.RequesterNav.PhoneNumber))
                .ForMember(d => d.Prescription, opt => opt.MapFrom(x => x.Prescription))
                .ForMember(d => d.BloodComponent, opt => opt.MapFrom(x => x.BloodComponentNav.Description))
                .ForMember(d => d.BloodType, opt => opt.MapFrom(x => x.BloodTypeNav.Description))
                .ForMember(d => d.RequesterEmail, opt => opt.MapFrom(x => x.RequesterNav.Email))
                .ForMember(d => d.RequesterName, opt => opt.MapFrom(x => x.RequesterNav.FirstName + " " + x.RequesterNav.LastName))
                .ForMember(d => d.RequesterPhone, opt => opt.MapFrom(x => x.RequesterNav.PhoneNumber));
            CreateMap<Request, RequestSummaryDto>()
                .ForMember(rs => rs.Province, opt => opt.MapFrom(r => r.ProvinceNav.Name))
                .ForMember(rs => rs.ResponseDueDate, opt => opt.MapFrom(r => r.ResponseDueDate.ToStringSP()));
            #endregion
        }
    }
}
