using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;

namespace UnaPinta.Core.Contracts
{
    public interface IUsersServices
    {
        //Task GenerateConfirmationCode(int userId);
        Task<ConfirmationResponse> ConfirmEmail(User userToConfirm, string code);
        Task SendConfirmationCode(int userId);
        
    }
}