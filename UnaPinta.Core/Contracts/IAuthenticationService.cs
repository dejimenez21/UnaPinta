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
        Task ConfirmEmailAsync(string id, string token);
        Task SendEmailConfirmationAsync(User user, string action);
        Task<bool> ValidateUser(UserLogin login);
        Task<string> CreateToken();

        Task<RoleCreateResponseDto> CreateRole(RoleCreate roleCreate);
    }
}
