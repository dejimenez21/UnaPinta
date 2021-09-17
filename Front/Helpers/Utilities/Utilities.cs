using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Helpers.Utilities
{
    public static class Utilities
    {
        public static string SetUserCookies(IRestResponse restResponse)
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(50);
            var obj = JObject.Parse(restResponse.Content);
            var getToken = obj["token"].ToString();
            return getToken;
        }
    }
}
