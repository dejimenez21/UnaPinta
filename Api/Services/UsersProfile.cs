using Api.Entities;
using Api.Models;
using AutoMapper;

namespace Api.Services
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            //CreateMap<Usuario, Register>();
            CreateMap<Register, User>();
        }
    }
}