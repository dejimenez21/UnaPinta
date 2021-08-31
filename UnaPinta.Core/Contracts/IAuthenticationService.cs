using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Dto.Models;

namespace UnaPinta.Core.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUser(UserLogin login);
        Task<string> CreateToken();

        Task<RoleCreateResponseDto> CreateRole(RoleCreate roleCreate);
        Task<bool> SendEmailConfirmation(UserSignUp user);
    }
}
