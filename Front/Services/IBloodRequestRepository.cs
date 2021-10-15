using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Models;
using UnaPinta.Dto.Models;
using UnaPinta.Dto.Models.Request;

namespace Una_Pinta.Services
{
    public interface IBloodRequestRepository
    {
        Task<IRestResponse> PostBloodRequest(RequestCreateDto requestCreate, string token);
        Task<IRestResponse> PostCase(Cases cases, string token);
        Task<RequestDetails> GetRequestDetails(int id, string token);
        Task<List<RequestSummary>> GetRequestSummary(string token);
        Task<List<RequestSummary>> GetRequestSummaryToDatatable(string token);
        Task<RequestCasesDto> GetRequestsWithDonors(string token, int id);
        Task<List<StringDate>> GetStringDates();
        Task<IRestResponse> PostCaseComplete(int id, string token);
        Task<IRestResponse> PostCaseCanceled(int id, string token);
        Task<IRestResponse> PostRequestCompleted(int id, string token);
    }
}
