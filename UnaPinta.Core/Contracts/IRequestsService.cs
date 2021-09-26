using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using UnaPinta.Dto.Models.Request;

namespace UnaPinta.Core.Contracts
{
    public interface IRequestsService
    {
        Task SendRequestNotification(Request request);
        Task<RequestDetailsDto> RetrieveRequestDetailsById(int id);
        Task<Func<Task>> CreateRequest(RequestCreate request, string userName);
        Task<IEnumerable<RequestSummaryDto>> RetrieveRequestsSummaryByDonor(string username);
    }
}