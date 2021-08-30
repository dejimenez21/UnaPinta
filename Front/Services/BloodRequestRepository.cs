﻿using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Requests;
using UnaPinta.Dto.Models;

namespace Una_Pinta.Services
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        public Task<RequestDetailsDto> GetRequestDetails(int id, string token)
        {
            try
            {
                var client = new RestClient(ApiRequests.HostUrl);
                client.Authenticator = new JwtAuthenticator(token);
                var request = new RestRequest(ApiRequests.GetRequestDetails(id), Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cache-Control", "no-cache");
                var response = client.ExecuteAsync(request).Result.Content;
                var content = JsonConvert.DeserializeObject<RequestDetailsDto>(response);
                return Task.FromResult(content);
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Response);
                return null;
            }
        }

        /// <summary>
        /// Post Request to API
        /// <param name="requestCreate">Request model</param>
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="System.WebException">Thrown when status code of response are different to 200 (OK)</exception>
        public Task<IRestResponse> PostBloodRequest(RequestCreate requestCreate, string token)
        {
            var client = new RestClient(ApiRequests.HostUrl);
            client.Authenticator = new JwtAuthenticator(token);
            var request = new RestRequest(ApiRequests.PostBloodRequest, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddJsonBody(requestCreate);
            try
            {
                var queryResult = client.ExecuteAsync(request).Result;
                return Task.FromResult(queryResult);
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Response);
                return null;
            }
        }
    }
}
