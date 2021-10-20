using System;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using UnaPinta.Data.Contracts;
using UnaPinta.Core.Contracts;
using UnaPinta.Core.Contracts.Users;
using UnaPinta.Dto.Models.User;
using Microsoft.AspNetCore.Identity;
using UnaPinta.Core.Exceptions;
using AutoMapper;

namespace UnaPinta.Core.Services
{
    public class UsersServices : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersServices(IUserRepository userRepository, UserManager<User> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> RetrieveUserProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new BaseDomainException("El usuario {username} no existe", 404);

            var profile = _mapper.Map<UserProfileDto>(user);
            return profile;
        }

        private async Task<ConfirmationCode> GenerateConfirmationCode(long userId)
        {
            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                builder.Append(rnd.Next(10));
            }

            var code = builder.ToString();

            ConfirmationCode confirmation = new ConfirmationCode();
            confirmation.Code = code;
            confirmation.UserId = userId;

            return confirmation;
        }
        
    }
}