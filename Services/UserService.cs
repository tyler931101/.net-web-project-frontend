using frontend.Models;

namespace frontend.Services
{
    public class UserService
    {
        private readonly ApiService _api;

        public UserService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            return await _api.GetAsync<List<UserModel>>("api/user") ?? new();
        }
    }
}