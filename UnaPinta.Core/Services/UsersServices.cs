using System;
using System.Text;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;
using UnaPinta.Data.Contracts;
using UnaPinta.Core.Contracts;

namespace UnaPinta.Core.Services
{
    public class UsersServices
    {
        private readonly EmailService _sender;
        private readonly IUnaPintaRepository _repo;

        public UsersServices(IUnaPintaRepository repo, EmailService sender)
        {
            _sender = sender;
            _repo = repo;
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
        
    }
}