using System.Threading.Tasks;
using Api.Entities;

namespace Api.Services
{
    public interface IRequestsService
    {
        Task SendRequestNotification(Request request);
    }
}