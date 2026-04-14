using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiAnnouncementService
    {
        public async Task<List<RecordModel>> GetAnnouncementsAsync()
        {
            var response = await ApiClient.Client.GetAsync("api/announcement");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return JsonSerializer.Deserialize<List<RecordModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            
        }

        public async Task<RecordModel> GetAnnouncementByIdAsync(string id)
        {
            var response = await ApiClient.Client.GetAsync($"api/announcement/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return JsonSerializer.Deserialize<RecordModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<string> AddAnnouncementAsync(RecordModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            var response = await ApiClient.Client.PostAsJsonAsync("api/announcement", model);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{responseContent}");

            var created = JsonSerializer.Deserialize<RecordModel>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return created?.Id ?? string.Empty;
        }

        public async Task DeleteAnnouncementAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/announcement/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{responseContent}");
        }
    }
}