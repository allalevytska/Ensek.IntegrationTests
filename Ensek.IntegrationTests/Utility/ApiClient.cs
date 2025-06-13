using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Ensek.IntegrationTests.Utility
{
    public sealed class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        public async Task<T> GetRequest<T>(string endpoint)
        {
            var url = new Uri(httpClient.BaseAddress + endpoint);

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent);
        }


        public async Task<TResult> PutRequest<T, TResult>(string endpoint, T body)
        {
            var url = new Uri(httpClient.BaseAddress + endpoint);

            var response = await httpClient.PutAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<TResult>(responseContent);
        }

        public async Task<TResult> PostRequest<T, TResult>(string endpoint, T body)
        {
            var url = new Uri(httpClient.BaseAddress + endpoint);

            var response = await httpClient.PostAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResult>(responseContent);
        }
    }
}
