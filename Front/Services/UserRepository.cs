using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Requests;
using Una_Pinta.Models;

namespace Una_Pinta.Services
{
    public class UserRepository : IUserRepository
    {
        public Task<UserSignUp> GetUser()
        {
            return null;
        }

        /// <summary>
        /// Post Request to API
        /// <param name="userSignUp">User Model</param>
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="System.WebException">Thrown when status code of response are different to 200 (OK)</exception>
        public async Task PostUser(UserSignUp userSignUp)
        {
            var client = new RestClient(ApiRequests.HostUrl);
            var request = new RestRequest(ApiRequests.PostUserEndPoint, Method.POST);
            try
            {
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddJsonBody(userSignUp);
                client.Execute(request);
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Response);
            }
        }
    }
}
