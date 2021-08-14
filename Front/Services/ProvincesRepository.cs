using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Requests;
using Una_Pinta.Models;

namespace Una_Pinta.Services
{
    public class ProvincesRepository : IProvincesRepository
    {
        /// <summary>
        /// Get Request to API
        /// </summary>
        /// <returns>List of provinces</returns>
        /// <exception cref="System.WebException">Thrown when status code of response are different to 200 (OK)</exception>
        public Task<List<Provinces>> GetProvinces()
        {
            try
            {
                var client = new RestClient(ApiRequests.HostUrl);
                var request = new RestRequest(ApiRequests.GetProvinces, Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cache-Control", "no-cache");
                var response = client.ExecuteAsync(request).Result.Content;
                var content = JsonConvert.DeserializeObject<List<Provinces>>(response);
                return Task.FromResult(content);
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Response);
                return null;
            }
        }
    }
}
