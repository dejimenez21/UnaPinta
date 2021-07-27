﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Dto.Models;

namespace UnaPinta.Core.Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserLogin login);
        Task<string> CreateToken();
    }
}
