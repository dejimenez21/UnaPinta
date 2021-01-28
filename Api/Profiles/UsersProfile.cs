using Api.Entities;
using Api.Models;
using AutoMapper;

namespace Api.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            //CreateMap<Usuario, Register>();
            CreateMap<Register, User>();
            CreateMap<Register, Role>();
        }
    }
}