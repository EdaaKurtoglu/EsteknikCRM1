using EsteknikCRM1.Models;
using EsteknikCRM1.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiAuthService
    {
        private readonly HttpClient _httpClient;

        public ApiAuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiConfig.BaseUrl)
            };
        }
        public async Task<List<UserModel>> GetUsersAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<UserModel>>("api/auth/users")
                   ?? new List<UserModel>();
        }

        public async Task<string> AddUserAsync(UserModel user)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/auth/users", user);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            var createdUser = JsonSerializer.Deserialize<UserModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return createdUser?.Id ?? string.Empty;
        }
        public async Task<UserModel> LoginAsync(string email, string password, string userRole)
        {
            var payload = new
            {
                UserMail = email,
                Password = password,
                UserRole = userRole
            };

            var response = await ApiClient.Client.PostAsJsonAsync("api/auth/login", payload);
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n\n{content}");

            return JsonSerializer.Deserialize<UserModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var payload = new
            {
                UserId = userId,
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            };

            var response = await ApiClient.Client.PostAsJsonAsync("api/auth/change-password", payload);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
        }
    }
}