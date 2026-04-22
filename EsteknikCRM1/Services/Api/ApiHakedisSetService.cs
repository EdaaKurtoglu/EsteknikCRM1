using EsteknikCRM1.Models;
using System.Text.Json;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EsteknikCRM1.Services.Api
{
    public class ApiHakedisSetService
    {
        public async Task<List<HakedisSetModel>> GetHakedisSetsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<HakedisSetModel>>("api/hakedissets")
                   ?? new List<HakedisSetModel>();
        }

        public async Task<string> AddHakedisSetAsync(HakedisSetModel model)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/hakedissets", model);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            var created = JsonSerializer.Deserialize<HakedisSetModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return created?.Id ?? string.Empty;
        }
    }
}