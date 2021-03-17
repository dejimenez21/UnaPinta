using System.Threading.Tasks;
using Api.Entities;

namespace Api.Contracts
{
    public interface IRequestsService
    {
        Task SendRequestNotification(Request request);
        Task CreateRequest(Request request, string userName);
    }
}