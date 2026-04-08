using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiAnnouncementService
    {
        public async Task<List<RecordModel>> GetAnnouncementsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<RecordModel>>("api/announcements")
                   ?? new List<RecordModel>();
        }

        public async Task<RecordModel> GetAnnouncementByIdAsync(string id)
        {
            return await ApiClient.Client.GetFromJsonAsync<RecordModel>($"api/announcements/{id}");
        }

        public async Task<string> AddAnnouncementAsync(RecordModel model)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/announcements", model);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");

            var created = await response.Content.ReadFromJsonAsync<RecordModel>();
            return created?.Id ?? string.Empty;
        }

        public async Task DeleteAnnouncementAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/announcements/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");
        }
    }
}