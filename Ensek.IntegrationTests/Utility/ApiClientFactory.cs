using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Ensek.IntegrationTests.Models;
namespace Ensek.IntegrationTests.Utility
{
    public class ApiClientFactory
    {
        public static async Task<IApiClient> CreateWebApiClientAsync()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://qacandidatetest.ensek.io/ENSEK/");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var token = await GetBearerToken(httpClient);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return new ApiClient(httpClient);
        }

        private static async Task<string> GetBearerToken(HttpClient httpClient)
        {
            var loginRequestBody = new
            {
                username = "test",
                password = "testing"
            };

            var jsonBody = JsonSerializer.Serialize(loginRequestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(EndpointRoute.Login, content);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var responseJson = JsonSerializer.Deserialize<LoginResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return responseJson?.AccessToken ?? throw new Exception("Failed to retrieve Bearer token.");
        }

    }
}
