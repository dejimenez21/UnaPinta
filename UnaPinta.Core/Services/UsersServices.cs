using System;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using UnaPinta.Data.Contracts;
using UnaPinta.Core.Contracts;

namespace UnaPinta.Core.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly IUnaPintaRepository _repo;

        public UsersServices(IUnaPintaRepository repo)
        {
            _repo = repo;
        }

        public async Task<ConfirmationResponse> ConfirmEmail(User userToConfirm, string code)
        {
            var matchingCode = await _repo.GetCodeByUser(code, userToConfirm.Id);
            var response = new ConfirmationResponse();
            if(matchingCode == null)
            {
                response.Confirmed = false; 
                response.Message = "Code is incorrect";
                return response;
            }

            if(matchingCode.ExpiresAt < DateTime.Now)
            {
                response.Confirmed = false; 
                response.Message = "Code has expired";
                return response;
            }
            
            //userToConfirm.Confirmed = true;
            await _repo.SaveChangesAsync();

            response.Confirmed = true;
            response.Message = "User's email confirmed successfully";
            return response;
                
        }

        private async Task<ConfirmationCode> GenerateConfirmationCode(long userId)
        {
            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                builder.Append(rnd.Next(10));
            }

            var code = builder.ToString();

            ConfirmationCode confirmation = new ConfirmationCode();
            confirmation.Code = code;
            confirmation.UserId = userId;
            
            _repo.AddConfirmationCode(confirmation);
            await _repo.SaveChangesAsync();

            return confirmation;
        }

        public async Task SendConfirmationCode(long userId)
        {
            var confirmation = await GenerateConfirmationCode(userId);

            EmailSender sender = new EmailSender(_repo);

            await sender.SendConfirmation(confirmation);
        }

        
    }
}