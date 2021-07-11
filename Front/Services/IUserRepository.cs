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
        Task<UserSignUp> GetUser(UserSignUp userSignUp);
        Task PostUser(UserSignUp userSignUp);
    }
}
