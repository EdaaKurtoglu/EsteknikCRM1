using EsteknikCRM1.Models;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace EsteknikCRM1.Services.Api
{
    public class ApiWorkflowFileService
    {
        public async Task<List<WorkflowFileModel>> UploadFilesAsync(string workflowId, List<string> filePaths)
        {
            var form = new MultipartFormDataContent();

            foreach (var path in filePaths)
            {
                var bytes = File.ReadAllBytes(path);
                var fileContent = new ByteArrayContent(bytes);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                form.Add(fileContent, "files", Path.GetFileName(path));
            }

            var response = await ApiClient.Client.PostAsync($"api/workflowfiles/upload/{workflowId}", form);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return JsonSerializer.Deserialize<List<WorkflowFileModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<WorkflowFileModel>();
        }

        public async Task<List<WorkflowFileModel>> GetFilesByWorkflowIdAsync(string workflowId)
        {
            var response = await ApiClient.Client.GetAsync($"api/workflowfiles/{workflowId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return JsonSerializer.Deserialize<List<WorkflowFileModel>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<WorkflowFileModel>();
        }
    }
}