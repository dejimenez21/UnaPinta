using Api.Entities;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<RequestCreate, Request>();
        }
    }
}