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
        public async Task<List<LaborOperationModel>> GetLaborOperationsAsync()
        {
            var response = await ApiClient.Client.GetAsync("api/operations/labor-operations");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return System.Text.Json.JsonSerializer.Deserialize<List<LaborOperationModel>>(
                content,
                new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<LaborOperationModel>();
        }

        public async Task<decimal> GetOperationPriceAsync(string productCode, string laborCode)
        {
            string url =
                $"api/operations/price?productCode={Uri.EscapeDataString(productCode ?? "")}&laborCode={Uri.EscapeDataString(laborCode ?? "")}";

            var response = await ApiClient.Client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            if (decimal.TryParse(content, out var price))
                return price;

            return 0;
        }
        /*public async Task<decimal> GetOperationPriceAsync(string stockCode, string operationType)
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
        */
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
        public async Task MarkOperationsAsBilledAsync(string hakedisSetId, List<string> ids)
        {
            var payload = new
            {
                HakedisSetId = hakedisSetId,
                OperationIds = ids
            };

            var response = await ApiClient.Client.PutAsJsonAsync(
                "api/operations/workflow-team-operations/mark-billed",
                payload);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
        }
        public async Task<List<WorkflowTeamOperationSaveModel>> GetOperationsByHakedisSetIdAsync(string setId)
        {
            return await ApiClient.Client
                .GetFromJsonAsync<List<WorkflowTeamOperationSaveModel>>(
                    $"api/operations/workflow-team-operations/by-hakedis-set/{setId}")
                ?? new List<WorkflowTeamOperationSaveModel>();
        }
    }
}