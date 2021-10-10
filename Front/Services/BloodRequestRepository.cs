using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Requests;
using Una_Pinta.Models;
using UnaPinta.Dto.Models;

namespace Una_Pinta.Services
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        /// <summary>
        /// Post Request to API
        /// <param name="id">Id of request</param>
        /// <param name="token">JWT parameter</param>
        /// </summary>
        /// <returns>RequestDetails object</returns>
        /// <exception cref="System.WebException">Thrown when status code of response are different to 200 (OK)</exception>
        public Task<RequestDetails> GetRequestDetails(int id, string token)
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
                var content = JsonConvert.DeserializeObject<RequestDetails>(response);
                return Task.FromResult(content);
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Response);
                return null;
            }
        }

        /// <summary>
        /// Get Request to API
        /// <param name="token">JWT parameter</param>
        /// </summary>
        /// <returns>RequestSummary</returns>
        /// <exception cref="System.WebException">Thrown when status code of response are different to 200 (OK)</exception>
        public Task<List<RequestSummary>> GetRequestSummary(string token)
        {
            try
            {
                var client = new RestClient(ApiRequests.HostUrl);
                client.Authenticator = new JwtAuthenticator(token);
                var request = new RestRequest(ApiRequests.GetRequestSummary, Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cache-Control", "no-cache");
                var response = client.ExecuteAsync(request).Result.Content;
                var content = JsonConvert.DeserializeObject<List<RequestSummary>>(response);
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
        /// <param name="token">JWT parameter</param>
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        /// <exception cref="System.WebException">Thrown when status code of response are different to 200 (OK)</exception>
        public Task<IRestResponse> PostBloodRequest(RequestCreateDto requestCreate, string token)
        {
            var client = new RestClient(ApiRequests.HostUrl);
            client.Authenticator = new JwtAuthenticator(token);
            var request = new RestRequest(ApiRequests.PostBloodRequest, Method.POST);            
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddParameter("application/json", $"requestCreate={JsonConvert.SerializeObject(requestCreate)}", ParameterType.RequestBody);
            //request.AddBody(requestCreate);
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

        /// <summary>
        /// Get Request to API
        /// </summary>
        /// <returns>List of stringDates</returns>
        /// <exception cref="System.WebException">Thrown when status code of response are different to 200 (OK)</exception>
        public Task<List<StringDate>> GetStringDates()
        {
            try
            {
                var client = new RestClient(ApiRequests.HostUrl);
                var request = new RestRequest(ApiRequests.GetStringDates, Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cache-Control", "no-cache");
                var response = client.ExecuteAsync(request).Result.Content;
                var content = JsonConvert.DeserializeObject<List<StringDate>>(response);
                return Task.FromResult(content);
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Response);
                return null;
            }
        }

        public Task<IRestResponse> PostCase(Cases cases, string token)
        {
            var client = new RestClient(ApiRequests.HostUrl);
            client.Authenticator = new JwtAuthenticator(token);
            var request = new RestRequest(ApiRequests.CreateCase, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddJsonBody(cases);
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
