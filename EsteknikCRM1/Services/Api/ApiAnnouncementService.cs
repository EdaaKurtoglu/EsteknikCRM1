using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

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
            var response = await ApiClient.Client.PostAsJsonAsync("api/announcement", model);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            var created = JsonSerializer.Deserialize<RecordModel>(content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return created?.Id ?? "";
        }
        public async Task<List<AnnouncementFileModel>> GetAnnouncementFilesAsync(string announcementId)
        {
            return await ApiClient.Client.GetFromJsonAsync<List<AnnouncementFileModel>>(
                $"api/announcement/files/{announcementId}")
                ?? new List<AnnouncementFileModel>();
        }

        public async Task DownloadAnnouncementFileAsync(string fileId, string savePath)
        {
            var response = await ApiClient.Client.GetAsync($"api/announcement/download/{fileId}");
            var content = await response.Content.ReadAsByteArrayAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception("Dosya indirilemedi.");

            File.WriteAllBytes(savePath, content);
        }
        public async Task DeleteAnnouncementAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/announcement/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{responseContent}");
        }
        public async Task UploadFilesAsync(string announcementId, List<string> filePaths)
        {
            using (var form = new MultipartFormDataContent())
            {
                foreach (var path in filePaths)
                {
                    var bytes = File.ReadAllBytes(path);

                    var fileContent = new ByteArrayContent(bytes);
                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                    form.Add(fileContent, "files", Path.GetFileName(path));
                }

                var response = await ApiClient.Client.PostAsync(
                    $"api/announcement/upload/{announcementId}", form);

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Dosya yükleme hatası:\n{content}");
            }
        }
    }
}