using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Una_Pinta.Helpers.Requests
{
    public static class ApiRequests
    {
        public static string HostUrl = "https://localhost:44393/";
        public static string PostUserSignup = "api/Auth/signup";
        public static string GetUserLogin = "api/Auth/login";
        public static string GetProvinces = "api/provinces";

        public static string GetBloodTypesPossibles(int idBlood)
        {
            return $"api/BloodTypes/compatible/{idBlood}";
        }
    }
}
