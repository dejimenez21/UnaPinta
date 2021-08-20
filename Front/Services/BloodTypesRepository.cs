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
    public class BloodTypesRepository : IBloodTypesRepository
    {
        public Task<List<int>> GetBloodTypes(int id)
        {
            try
            {
                var client = new RestClient(ApiRequests.HostUrl);
                var request = new RestRequest(ApiRequests.GetBloodTypesPossibles(id), Method.GET);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Cache-Control", "no-cache");
                var response = client.ExecuteAsync(request).Result.Content;
                var content = JsonConvert.DeserializeObject<List<int>>(response);
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
