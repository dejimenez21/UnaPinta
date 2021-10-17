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
        public static string GetRequestSummaryDataTable = "api/Requests/datatable";
        public static string GetStringDates = "api/Requests/stringDates";
        public static string CreateCase = "api/cases";
        public static string DonorWithActiveCase = "api/cases/inprocess";

        public static string ResetPassword = "api/Auth/resetPassword";

        public static string GetCasesWithDonors(int id) => $"api/Requests/withCases/{id}";

        public static string SendEmailForResetPassword(string email) => $"api/Auth/sendPasswordReset/{email}";

        public static string ConfirmEmail(string id, string token) => $"api/auth/confirmemail?userid={id}&token={token}";

        public static string CompleteCase(int id) => $"api/Cases/complete/{id}";

        public static string CompleteRequest(int id) => $"api/Requests/markAsCompleted/{id}";

        public static string DeleteRequest(int id) => $"api/Requests/{id}";

        public static string CancelCase(int id) => $"api/Cases/cancel/{id}";

        public static string GetBloodTypesPossibles(int idBlood) => $"api/BloodTypes/compatible/{idBlood}";

        public static string GetRequestDetails(int idRequest) => $"api/Requests/{idRequest}/details";

        public static string ResendEmail() => $"api/auth/sendemailverification";

    }
}
