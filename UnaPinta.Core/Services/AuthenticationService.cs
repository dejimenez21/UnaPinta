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
using AutoMapper;
using UnaPinta.Core.Exceptions.Role;
using Microsoft.AspNetCore.WebUtilities;
using UnaPinta.Core.Exceptions;
using UnaPinta.Core.Exceptions.User;
using UnaPinta.Dto.Models.Auth;

namespace UnaPinta.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        //private User _user;
        public AuthenticationService(UserManager<User> userManager, IConfiguration configuration, 
             RoleManager<Role> roleManager, IMapper mapper, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _mapper = mapper;
            _emailService = emailService;
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
            else if (roleResult.Errors.Any(e => e.Code == "DuplicateRoleName"))
                throw new AlreadyExistsRoleException(roleResult.Errors.First().Description);
            else
                throw new Exception(roleResult.Errors.First().Description);
        }

        public async Task<string> CreateToken(User user)
        {
            var signinCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UserJwtClaimsDto.Name), $"{user.FirstName} {user.LastName}"),
                new Claim(nameof(UserJwtClaimsDto.UserName), user.UserName),
                new Claim(nameof(UserJwtClaimsDto.EmailConfirmed), user.EmailConfirmed.ToString()),
                new Claim(nameof(UserJwtClaimsDto.BloodType), ((int)user.BloodTypeId).ToString()),
                new Claim(nameof(UserJwtClaimsDto.BirthDate), user.BirthDate.ToString())
            };


            var roles = await _userManager.GetRolesAsync(user);
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

        public async Task<string> ConfirmEmailAsync(string id, string token)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(token))
                throw new EmailVerificationDataMissingException();

            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                throw new UserNotFoundException($"The user with the id {id} doesn't exist.", id);
            }

            byte[] decodedToken;
            try
            {
                decodedToken = WebEncoders.Base64UrlDecode(token);
            }
            catch(Exception e)
            {
                throw new TokenBadFormatException(e.Message);
            }

            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (!result.Succeeded)
            {
                if(result.Errors.Any(e => e.Code == "InvalidToken"))
                    throw new EmailVerificationTokenInvalidException();
            }

            return await CreateToken(user);
                
        }

        public async Task SendEmailConfirmationAsync(User user, string action)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedToken);

            var url = string.Concat(action, $"?id={user.Id}&token={validEmailToken}");

            await _emailService.SendEmailVerificationAsync(user, url);
        }

        public async Task<string> AuthenticateAsync(UserLogin login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);

            var isValidCredentials = user != null && await _userManager.CheckPasswordAsync(user, login.Password);
            if (!isValidCredentials)
                throw new InvalidCredentialsException();

            var token = await CreateToken(user);

            return token;
        }
    }
}
