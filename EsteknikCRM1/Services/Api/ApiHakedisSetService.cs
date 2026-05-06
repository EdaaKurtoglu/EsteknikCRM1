using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiHakedisSetService
    {
        public async Task<List<HakedisSetModel>> GetHakedisSetsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<HakedisSetModel>>("api/hakedisset")
                   ?? new List<HakedisSetModel>();
        }

        public async Task<string> AddHakedisSetAsync(HakedisSetModel model)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/hakedisset", model);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            var created = JsonSerializer.Deserialize<HakedisSetModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return created?.Id ?? string.Empty;
        }

        public async Task UpdateSetApproveDateAsync(string setId)
        {
            var model = new
            {
                SetApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var response = await ApiClient.Client.PutAsJsonAsync(
                $"api/hakedisset/{setId}/approve-date",
                model);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Set onay tarihi güncellenemedi: {response.StatusCode}\n{content}");
        }
    }
}