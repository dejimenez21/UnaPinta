using UnaPinta.Core.Contracts;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Dto.Models;
using AutoMapper;

namespace UnaPinta.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        private User _user;
        public AuthenticationService(UserManager<User> userManager, IConfiguration configuration, 
             RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleCreateResponseDto> CreateRole(RoleCreate roleCreate)
        {
            var roleName = roleCreate.RoleName;
            var newRole = new Role()
            {
                Name = roleName,
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
                return _mapper.Map<RoleCreateResponseDto>(newRole);
            else
                throw new Exception(roleResult.Errors.First().Description);
        }

        public async Task<string> CreateToken()
        {
            var signinCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> ValidateUser(UserLogin login)
        {
            _user = await _userManager.FindByNameAsync(login.UserName);

            return (_user != null && await _userManager.CheckPasswordAsync(_user, login.Password));
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim("Name", $"{_user.FirstName} {_user.LastName}"),
                new Claim("UserName", _user.UserName)
            };


            var roles = await _userManager.GetRolesAsync(_user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));   
            }

            return claims;
        }   

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: credentials
            );

            return tokenOptions;
        }
    }
}
