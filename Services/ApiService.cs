using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace frontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        // Generic GET
        public async Task<T?> GetAsync<T>(string url)
        {
            return await _http.GetFromJsonAsync<T>(url);
        }

        // Generic POST
        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            var response = await _http.PostAsJsonAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }

            throw new HttpRequestException($"Request failed: {response.StatusCode}");
        }

        // Generic POST (no response body)
        public async Task<bool> PostAsync<TRequest>(string url, TRequest data)
        {
            var response = await _http.PostAsJsonAsync(url, data);
            return response.IsSuccessStatusCode;
        }
    }
}