using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Una_Pinta.Models;

namespace Una_Pinta.Services
{
    public interface IUserRepository
    {
        Task<int> GetUser(UserSignUp userSignUp);
        Task<int> PostUser(UserSignUp userSignUp);
    }
}
