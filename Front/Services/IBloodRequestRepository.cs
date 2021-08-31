using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Models;
using UnaPinta.Dto.Models;

namespace Una_Pinta.Services
{
    public interface IBloodRequestRepository
    {
        Task<IRestResponse> PostBloodRequest(RequestCreate requestCreate, string token);
        Task<RequestDetails> GetRequestDetails(int id, string token);
    }
}
