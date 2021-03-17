using System.Threading.Tasks;
using Api.Entities;
using Api.Models;

namespace Api.Contracts
{
    public interface IUsersServices
    {
        //Task GenerateConfirmationCode(int userId);
        Task<ConfirmationResponse> ConfirmEmail(User userToConfirm, string code);
        Task SendConfirmationCode(int userId);
        
    }
}