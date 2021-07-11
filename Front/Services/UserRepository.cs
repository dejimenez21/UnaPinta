﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Requests;
using Una_Pinta.Models;

namespace Una_Pinta.Services
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Post Request to API
        /// <param name="userSignUp">User Model</param>
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="System.WebException">Thrown when status code of response are different to 200 (OK)</exception>
        public Task<int> GetUser(UserSignUp userSignUp)
        {
            var client = new RestClient(ApiRequests.HostUrl);
            var request = new RestRequest(ApiRequests.GetUserLogin, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddJsonBody(userSignUp);
            try
            {
                var queryResult = client.ExecuteAsync(request).Result.StatusCode;
                if (((int)queryResult) == 200)
                    return Task.FromResult(((int)queryResult));
                else
                    return Task.FromResult(((int)queryResult));
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Response);
                return null;
            }
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
            var request = new RestRequest(ApiRequests.PostUserSignup, Method.POST);
            try
            {
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddJsonBody(userSignUp);
                var responseapi = client.Execute(request);
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Response);
            }
        }


    }
}
