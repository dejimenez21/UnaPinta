using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Data.Entities;
using UnaPinta.Dto.Models;

namespace UnaPinta.Core.Contracts
{
    public interface IAuthenticationService
    {
        Task<string> ConfirmEmailAsync(string id, string token);
        Task SendEmailConfirmationAsync(User user, string action);
        Task<string> CreateToken(User user);
        Task<string> AuthenticateAsync(UserLogin login);

        Task<RoleCreateResponseDto> CreateRole(RoleCreate roleCreate);
        Task SendPasswordResetLinkAsync(string email, string action);
    }
}
