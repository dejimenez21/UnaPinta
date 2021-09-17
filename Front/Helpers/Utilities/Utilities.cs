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
        public static string SetUserCookies(IRestResponse restResponse)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(50)
            };
            var obj = JObject.Parse(restResponse.Content);
            var getToken = obj["token"].ToString();
            return getToken;
        }

        public static bool VerifiedToken(JwtSecurityToken token)
        {
            if (token.Claims.First(c => c.Type == "EmailConfirmed").Value.Contains("False"))
            {
                return false;
            }

            return true;
        }
    }
}
