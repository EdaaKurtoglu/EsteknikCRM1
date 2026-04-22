using EsteknikCRM1.Models;
using EsteknikCRM1.Models.EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiOperationService
    {
        public async Task<decimal> GetOperationPriceAsync(string stockCode, string operationType)
        {
            string url = $"api/operations/price?stockCode={Uri.EscapeDataString(stockCode ?? "")}&operationType={Uri.EscapeDataString(operationType ?? "")}";

            var response = await ApiClient.Client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            if (decimal.TryParse(content, out var price))
                return price;

            return 0;
        }

        public async Task AddWorkflowTeamOperationAsync(WorkflowTeamOperationSaveModel model)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/operations/workflow-team-operation", model);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
        }
        public async Task<List<string>> GetOperationTypesAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<string>>("api/operations/types")
                   ?? new List<string>();
        }
        public async Task<List<WorkflowTeamOperationSaveModel>> GetWorkflowTeamOperationsAsync()
        {
            return await ApiClient.Client
                .GetFromJsonAsync<List<WorkflowTeamOperationSaveModel>>("api/operations/workflow-team-operations")
                   ?? new List<WorkflowTeamOperationSaveModel>();
        }
        public async Task MarkOperationsAsBilledAsync(List<string> ids)
        {
            var response = await ApiClient.Client.PutAsJsonAsync(
                "api/operations/workflow-team-operations/mark-billed",
                ids);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
        }
    }
}