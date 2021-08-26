using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;

namespace UnaPinta.Core.Contracts
{
    public interface IRequestsService
    {
        Task SendRequestNotification(Request request);
        Task CreateRequest(Request request, string userName);
        Task<RequestDetailsDto> RetrieveRequestDetailsById(int id);
    }
}