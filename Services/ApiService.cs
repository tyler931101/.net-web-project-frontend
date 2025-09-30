using System.Net.Http;
using System.Net.Http.Json;

namespace frontend.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            return await _http.GetFromJsonAsync<T>(url);
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body)
        {
            var response = await _http.PostAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task PutAsync<TRequest>(string url, TRequest body)
        {
            var response = await _http.PutAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string url)
        {
            var response = await _http.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}