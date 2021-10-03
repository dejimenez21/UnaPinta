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
        public static string PostBloodRequest = "api/Requests";
        public static string GetUserLogin = "api/Auth/login";
        public static string GetProvinces = "api/provinces";
        public static string GetBloodComponent = "api/BloodComponent";
        public static string GetRequestSummary = "api/Requests/summary";
        public static string GetStringDates = "api/Requests/stringDates";

        public static string ConfirmEmail(string id, string token) => $"api/auth/confirmemail?userid={id}&token={token}";

        public static string GetBloodTypesPossibles(int idBlood) => $"api/BloodTypes/compatible/{idBlood}";

        public static string GetRequestDetails(int idRequest) => $"api/Requests/{idRequest}/details";

        public static string ResendEmail() => $"api/auth/sendemailverification";

    }
}
