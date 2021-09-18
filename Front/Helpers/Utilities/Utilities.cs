using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Services;
using UnaPinta.Dto.Enums;

namespace Una_Pinta.Helpers.Utilities
{
    public class Utilities
    {
        readonly IHttpContextAccessor _httpContextAccessor;
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
            var jwToken = new JwtSecurityToken(jwtEncodedString: token);
            return jwToken;
        }
    }
}
