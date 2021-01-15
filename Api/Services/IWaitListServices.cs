using System.Threading.Tasks;
using Api.Entities;

namespace Api.Services
{
    public interface IWaitListServices
    {
        Task CalculateAvailableAtDate(WaitList item);
    }
}