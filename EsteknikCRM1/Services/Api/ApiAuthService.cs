using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiAuthService
    {
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
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return JsonSerializer.Deserialize<UserModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}