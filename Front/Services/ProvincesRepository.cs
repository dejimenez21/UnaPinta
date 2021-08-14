using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Una_Pinta.Helpers.Requests;
using Una_Pinta.Models;

namespace Una_Pinta.Services
{
    public class ProvincesRepository : IProvincesRepository
    {
        public Task<List<Provinces>> GetProvinces()
        {
            var client = new RestClient(ApiRequests.HostUrlLocations);
            var request = new RestRequest(ApiRequests.GetProvinces, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InVzZXJfZW1haWwiOiJhYnJhaGFtbW9yaWxsbzc3N0BnbWFpbC5jb20iLCJhcGlfdG9rZW4iOiJiekhqMkpUQmUydi12SjF3cG9oTGNKaXVVcDd4SkVzRmdxUllQTi0yZE9DS2dKTlpraFhQRDhsSXBEYWRpelAwcmZNIn0sImV4cCI6MTYyODk5NzY3MH0.9T2tW_99SMepM6RC0X4PmjVp_iHUOuPNQkDEaMhiSAg");
            request.AddHeader("Accept", "application/json");
            var response = client.ExecuteAsync(request).Result.Content;
            var content = JsonConvert.DeserializeObject<List<Provinces>>(response);
            return Task.FromResult(content);
        }
    }
}
