using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Helpers.Requests
{
    public static class ApiRequests
    {
        public static string HostUrl = "https://localhost:44393/";
        public static string HostUrlLocations = "https://www.universal-tutorial.com";
        public static string PostUserSignup = "api/Auth/signup";
        public static string GetUserLogin = "api/Auth/login";
        public static string GetProvinces = string.Concat("api/states/Dominican", " Republic");
    }
}
