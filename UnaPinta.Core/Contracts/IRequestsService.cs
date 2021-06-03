using System.Threading.Tasks;
using UnaPinta.Data.Entities;

namespace UnaPinta.Core.Contracts
{
    public interface IRequestsService
    {
        Task SendRequestNotification(Request request);
        Task CreateRequest(Request request, string userName);
    }
}