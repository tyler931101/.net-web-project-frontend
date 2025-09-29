using System.Net.Http.Json;
using frontend.Models;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;

namespace frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigation;
        private readonly CustomAuthStateProvider _authProvider;

        public AuthService(
            HttpClient http,
            IJSRuntime jsRuntime,
            NavigationManager navigation,
            AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _jsRuntime = jsRuntime;
            _navigation = navigation;

            // Cast AuthenticationStateProvider to our custom provider
            _authProvider = (CustomAuthStateProvider)authStateProvider;
        }

        public async Task<string> Register(RegisterModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);
            if (response.IsSuccessStatusCode)
            {
                return "✅ Registration successful!";
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return $"❌ {errorMessage}";
            }
        }

        public async Task<string> Login(LoginModel model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var authResult = await response.Content.ReadFromJsonAsync<AuthResult>();

                if (authResult != null && authResult.IsSuccess && !string.IsNullOrEmpty(authResult.Message))
                {
                    var token = authResult.Message;

                    // Save token
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);

                    // Attach token
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);

                    // ✅ Update Blazor auth state
                    _authProvider.MarkUserAsAuthenticated(model.Email);

                    _navigation.NavigateTo("/home");
                    return "✅ Login successful!";
                }
            }

            var errorMessage = await response.Content.ReadAsStringAsync();
            return $"❌ {errorMessage}";
        }

        public async Task Logout()
        {
            // Clear token
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");

            // Update Blazor auth state
            await _authProvider.MarkUserAsLoggedOut();

            // Clear header
            _http.DefaultRequestHeaders.Authorization = null;

            // Redirect
            _navigation.NavigateTo("/auth/login", true);
        }
    }
}