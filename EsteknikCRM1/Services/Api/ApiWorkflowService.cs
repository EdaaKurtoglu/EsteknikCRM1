using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiWorkflowService
    {
        public async Task<List<WorkflowModel>> GetWorkflowsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<WorkflowModel>>("api/workflow")
                   ?? new List<WorkflowModel>();
        }
        
        public async Task UpdateWorkflowAsync(string id, WorkflowModel workflow)
        {
            var response = await ApiClient.Client.PutAsJsonAsync($"api/workflow/{id}", workflow);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteWorkflowAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/workflow/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<WorkflowModel>> GetCompletedWorkflowsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<WorkflowModel>>("api/workflow/completed")
                   ?? new List<WorkflowModel>();
        }
       

            public async Task<string> AddWorkflowAsync(WorkflowModel workflow)
            {
                var response = await ApiClient.Client.PostAsJsonAsync("api/workflow", workflow);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");

                var createdWorkflow = await response.Content.ReadFromJsonAsync<WorkflowModel>();
                return createdWorkflow?.Id ?? string.Empty;
            }
        public async Task UpdateWorkflowStatusAsync(string id, string status)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(status),
                Encoding.UTF8,
                "application/json");

            var response = await ApiClient.Client.PutAsync($"api/workflow/{id}/status", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");
        }

        public async Task UpdateWorkflowTeamAsync(string id, string team)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(team),
                Encoding.UTF8,
                "application/json");

            var response = await ApiClient.Client.PutAsync($"api/workflow/{id}/team", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");
        }
        public async Task<WorkflowModel> GetWorkflowByIdAsync(string id)
        {
            var response = await ApiClient.Client.GetAsync($"api/workflow/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return JsonSerializer.Deserialize<WorkflowModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
    }

