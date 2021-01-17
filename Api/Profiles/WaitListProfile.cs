using AutoMapper;
using Api.Models;
using Api.Entities;

namespace Api.Profiles
{
    public class WaitListProfile : Profile
    {
        public WaitListProfile()
        {
            CreateMap<WaitListItemCreate, WaitList>();
        }     
    }
}