using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shared.Exceptions;

namespace Shared.HttpService
{
    public class HttpClientService
    {
        private readonly HttpClient _client;

        public HttpClientService()
        {
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Post(string uri, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> Get(string uri)
        {
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> Send(string uri, string method,string json)
        {

            HttpRequestMessage message = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = GetMethod(method),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var response = await _client.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private HttpMethod GetMethod(string method)
        {
            switch (method)
            {
                case "Get":
                    return HttpMethod.Get;
                case "Post":
                    return HttpMethod.Post;
                case "Put":
                    return HttpMethod.Put;
                case "Delete":
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Get;
            }                
        }
    }
}
