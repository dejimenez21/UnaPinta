using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserLogin login);
        Task<string> CreateToken();
    }
}
