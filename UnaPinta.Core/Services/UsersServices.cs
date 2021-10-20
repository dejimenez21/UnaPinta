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
        private readonly IProvinceService _provinceService;

        public UsersServices(IUserRepository userRepository, UserManager<User> userManager, IMapper mapper, IProvinceService provinceService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _provinceService = provinceService;
        }

        public async Task<UserProfileDto> RetrieveUserProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new BaseDomainException("El usuario {username} no existe", 404);

            var profile = _mapper.Map<UserProfileDto>(user);
            return profile;
        }

        public async Task<UserProfileDto> UpdateUserProfileInfo(UpdateUserProfileDto dto, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new BaseDomainException($"El usuario {username} no existe.", 404);

            if(!string.IsNullOrEmpty(dto.ProvinceCode))
            {
                var province = await _provinceService.RetrieveProvinceByCode(dto.ProvinceCode);
                if (province == null) throw new BaseDomainException("La provincia especificada no existe.", 400);
                user.ProvinceId = province.Id;
            }

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
            {
                user.PhoneNumber = dto.PhoneNumber;
            }

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

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