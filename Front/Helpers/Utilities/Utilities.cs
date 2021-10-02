using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Una_Pinta.Services;
using UnaPinta.Dto.Enums;

namespace Una_Pinta.Helpers.Utilities
{
    public class Utilities
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public RoleEnum roleEnum;
        public bool emailVerified = false;
        public int bloodType = 0;
        public string userName;
        public string name;
        public Utilities(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string SetSession(IRestResponse restResponse)
        {
            var parseContent = JObject.Parse(restResponse.Content);
            var token = parseContent["token"].ToString();
            _httpContextAccessor.HttpContext.Session.SetString("userToken", token);
            return _httpContextAccessor.HttpContext.Session.GetString("userToken");
        }

        public bool VerifyEmail(JwtSecurityToken token)
        {
            if (token.Claims.First(c => c.Type == "EmailConfirmed").Value.Contains("False"))
            {
                return false;
            }
            return true;
        }

        public RoleEnum VerifyRole(JwtSecurityToken token)
        {
            if (token.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Contains("solicitante"))
            {
                return RoleEnum.Solicitante;
            }
            else if (token.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Contains("donante"))
            {
                return RoleEnum.Donante;
            }
            return RoleEnum.Administrador;
        }

        public JwtSecurityToken GetJwtToken(string token)
        {
            return new JwtSecurityToken(jwtEncodedString: token);
        }

        public void SetUserName(JwtSecurityToken token)
        {
            var getUserName = token.Claims.First(c => c.Type == "UserName").Value;
            if (!string.IsNullOrEmpty(getUserName))
            {
                _httpContextAccessor.HttpContext.Session.SetString("userName", getUserName);
            }
        }

        public int VerifyBloodType(JwtSecurityToken token)
        {
            var selectBloodType = BloodComponentFill.BloodComponentFill.LoadBloodTypes().Find(e => e.Value == token.Claims.First(c => c.Type == "BloodType").Value);
            return Convert.ToInt32(selectBloodType.Value);
        }

        public (RoleEnum role, string userName, string name, int bloodType, bool emailVerified, DateTime birthDate) GetUserInfo(JwtSecurityToken token)
        {
            if (token.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Contains("solicitante"))
            {
                roleEnum = RoleEnum.Solicitante;
            }
            else if (token.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value.Contains("donante"))
            {
                roleEnum = RoleEnum.Donante;
            }

            name = token.Claims.First(c => c.Type == "Name").Value;

            var selectBloodType = BloodComponentFill.BloodComponentFill.LoadBloodTypes().Find(e => e.Value == token.Claims.First(c => c.Type == "BloodType").Value);
            bloodType = Convert.ToInt32(selectBloodType.Value);

            userName = token.Claims.First(c => c.Type == "UserName").Value;

            emailVerified = token.Claims.First(c => c.Type == "EmailConfirmed").Value.Contains("False");

            var birth = Convert.ToDateTime(token.Claims.First(c => c.Type == "BirthDate").Value);

            return new (roleEnum, userName, name, bloodType, emailVerified, birth);
        }
    }
}
