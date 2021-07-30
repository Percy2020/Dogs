using Microsoft.AspNetCore.Mvc;
using MiracleMileAPI.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiracleMileAPI.Http
{
    public class MakeHttpRequest
    {
        private readonly IHttpClientFactory _clientFactory;

        public MakeHttpRequest(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<T> SendRequest<T>(string url, string token, T body)
        {
            HttpRequestMessage request;

            if (body != null)
            {
                 request = new HttpRequestMessage(HttpMethod.Post, url);
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Get, url);
            }
      
            request.Headers.Add("Authorization", "Bearer "+ token);
            request.Headers.Add("Accept", "application/json");

            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            if (body != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }

            // HttpClient _client = new HttpClient();

            var client = _clientFactory.CreateClient();

            //var client = _client;

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                //var responseStream = await response.Content.ReadAsStreamAsync();
                //var test = responseStream;
                if (!String.IsNullOrEmpty(responseStream))
                { 
                JObject json = JObject.Parse(responseStream);
                //var Branches = await JsonSerializer.DeserializeAsync<User>(responseStream);
                return JsonConvert.DeserializeObject<T>(responseStream);
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                throw new Exception(response.ReasonPhrase);

            }
        }


        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://marty-dev.test-danicapension.se/api/selfservice/v1/customer/profile");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxOTUwMDExODIwNDYiLCJqdGkiOiI1MDI2ZjY2ZC1iZGFmLTRmZDQtOTljNC0wY2I4MzFmYTdmMmQiLCJpYXQiOjE1NzM4MTI1OTUsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiIxOTUwMDExODIwNDYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE5NTAwMTE4MjA0NiIsImh0dHA6Ly9zY2hlbWFzLmRhbmljYS5zZS9pZGVudGl0eS9jbGFpbXMvZGlzcGxheW5hbWUiOiJBcm5lIEFybmVzc29uIiwiaHR0cDovL3NjaGVtYXMuZGFuaWNhLnNlL2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiQXJuZXNzb24iLCJodHRwOi8vc2NoZW1hcy5kYW5pY2Euc2UvaWRlbnRpdHkvY2xhaW1zL2dpdmVubmFtZSI6IkFybmUiLCJodHRwOi8vc2NoZW1hcy5kYW5pY2Euc2UvaWRlbnRpdHkvY2xhaW1zL2lzc3VlciI6IkRhbmljYUJhbmtJZEFQSS1UZXN0IiwiaHR0cDovL3NjaGVtYXMuZGFuaWNhLnNlL2lkZW50aXR5L2NsYWltcy9hdWRpZW5jZSI6IkRhbmljYS1UZXN0IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoicHJpdmF0cGVyc29uIiwiaHR0cDovL3NjaGVtYXMuZGFuaWNhLnNlL2lkZW50aXR5L2NsYWltcy9hY3RvbmJlaGFsZm9mIjoiMTk1MDAxMTgyMDQ2IiwibmJmIjoxNTczODEyNTk1LCJleHAiOjE1NzM4MTQzOTUsImlzcyI6IkRhbmljYUJhbmtJZEFQSS1UZXN0IiwiYXVkIjoiRGFuaWNhLVRlc3QifQ.38bv6rFSb3iVaBbdmd-46GfHJOdBDJdG03WvJ3DtVOY");
            request.Headers.Add("Accept", "application/json");

            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

           // HttpClient _client = new HttpClient();

            var client = _clientFactory.CreateClient();

            //var client = _client;

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                //var responseStream = await response.Content.ReadAsStreamAsync();
                //var test = responseStream;

                //JObject json = JObject.Parse(responseStream);
                //var Branches = await JsonSerializer.DeserializeAsync<User>(responseStream);
                var tesdata = JsonConvert.DeserializeObject<User>(responseStream); ;
            }
            else
            {
                var Error = true;

            }
        }

        public async Task OnGet2()
        {
            HttpClient client = new HttpClient();

            var responseString = await client.GetStringAsync("https://marty-dev.test-danicapension.se/api/selfservice/v1/customer/profile");
        }
    }
}
