using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.JSInterop;

namespace frontend.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;

        public CustomAuthStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (string.IsNullOrEmpty(token))
            {
                var anonymous = new ClaimsIdentity();
                return new AuthenticationState(new ClaimsPrincipal(anonymous));
            }

            // For simplicity: just mark as authenticated (no JWT parsing here)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "User") // You can decode JWT to get actual claims
            };

            var identity = new ClaimsIdentity(claims, "jwtAuthType");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "User") }, "jwtAuthType");
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            var anonymous = new ClaimsIdentity();
            var user = new ClaimsPrincipal(anonymous);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}