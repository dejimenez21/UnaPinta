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

        public bool VerifiedToken(JwtSecurityToken token)
        {
            if (token.Claims.First(c => c.Type == "EmailConfirmed").Value.Contains("False"))
            {
                return false;
            }

            return true;
        }

        public JwtSecurityToken GetJwtToken(string token)
        {
            var jwToken = new JwtSecurityToken(jwtEncodedString: token);
            return jwToken;
        }
    }
}
