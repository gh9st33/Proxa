using System;
using System.Net.Http;
using System.Threading.Tasks;
using Proxa.Models;

namespace Proxa.Integration
{
    public class RapidAPIIntegration
    {
        private readonly HttpClient _httpClient;

        public RapidAPIIntegration(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://rapidapi.com/");
        }

        public async Task<Proxy> AddProxyAsync(string ipAddress, int port)
        {
            var response = await _httpClient.GetAsync($"api/proxy/add?ipAddress={ipAddress}&port={port}");

            if (response.IsSuccessStatusCode)
            {
                var proxy = new Proxy
                {
                    Id = Guid.NewGuid(),
                    IPAddress = ipAddress,
                    Port = port,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                return proxy;
            }

            return null;
        }

        public async Task<Proxy> GetProxyAsync(Guid proxyId)
        {
            var response = await _httpClient.GetAsync($"api/proxy/get/{proxyId}");

            if (response.IsSuccessStatusCode)
            {
                var proxy = await response.Content.ReadAsAsync<Proxy>();
                return proxy;
            }

            return null;
        }

        public async Task<bool> DeleteProxyAsync(Guid proxyId)
        {
            var response = await _httpClient.DeleteAsync($"api/proxy/delete/{proxyId}");

            return response.IsSuccessStatusCode;
        }
    }
}
