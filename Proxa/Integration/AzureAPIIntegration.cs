using System;
using System.Net.Http;
using System.Threading.Tasks;
using Proxa.Models;

namespace Proxa.Integration
{
    public class AzureAPIIntegration
    {
        private readonly HttpClient _httpClient;
        private readonly string _azureAPIKey;

        public AzureAPIIntegration(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _azureAPIKey = Environment.GetEnvironmentVariable("AZURE_API_KEY");
        }

        public async Task<Proxy> CheckProxyAsync(Proxy proxy)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://azure-api-url/check-proxy?ip={proxy.IP}&port={proxy.Port}");
            request.Headers.Add("Authorization", $"Bearer {_azureAPIKey}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // Deserialize the result and update the proxy object accordingly
                // For example:
                // proxy.Status = JsonConvert.DeserializeObject<ProxyStatus>(result);
            }
            else
            {
                // Handle the error response
                // For example:
                // throw new Exception("Error checking proxy with Azure API");
            }

            return proxy;
        }
    }
}
