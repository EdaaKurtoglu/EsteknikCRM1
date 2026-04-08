using EsteknikCRM1.Models;
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
            return await ApiClient.Client.GetFromJsonAsync<List<WorkflowModel>>("api/workflows")
                   ?? new List<WorkflowModel>();
        }

        public async Task<WorkflowModel> GetWorkflowByIdAsync(string id)
        {
            return await ApiClient.Client.GetFromJsonAsync<WorkflowModel>($"api/workflows/{id}");
        }

        
        public async Task UpdateWorkflowAsync(string id, WorkflowModel workflow)
        {
            var response = await ApiClient.Client.PutAsJsonAsync($"api/workflows/{id}", workflow);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteWorkflowAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/workflows/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<WorkflowModel>> GetCompletedWorkflowsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<WorkflowModel>>("api/workflows/completed")
                   ?? new List<WorkflowModel>();
        }
       

            public async Task<string> AddWorkflowAsync(WorkflowModel workflow)
            {
                var response = await ApiClient.Client.PostAsJsonAsync("api/workflows", workflow);
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

            var response = await ApiClient.Client.PutAsync($"api/workflows/{id}/status", content);
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

            var response = await ApiClient.Client.PutAsync($"api/workflows/{id}/team", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");
        }
    }
    }

