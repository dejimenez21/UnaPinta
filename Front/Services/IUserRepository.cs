using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Una_Pinta.Models;
using UnaPinta.Dto.Models.Auth;
using UnaPinta.Dto.Models.User;

namespace Una_Pinta.Services
{
    public interface IUserRepository
    {
        Task<IRestResponse> GetUser(UserSignUp userSignUp);
        Task<IRestResponse> PostUser(UserSignUp userSignUp);
        Task<IRestResponse> ConfirmEmail(string id, string token);
        Task<IRestResponse> ResendEmail(string token);
        Task<IRestResponse> SendEmail(string email);
        Task<IRestResponse> ResetPassword(PasswordResetDto passwordResetDto);
        Task<UserProfileDto> GetUserProfile(string token);
    }
}
