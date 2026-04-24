using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiProductLaborPriceService
    {
        public async Task<List<ProductLaborPriceModel>> GetAllAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<ProductLaborPriceModel>>("api/productlaborprices")
                   ?? new List<ProductLaborPriceModel>();
        }

        public async Task<ProductLaborPriceModel> AddAsync(ProductLaborPriceModel model)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/productlaborprices", model);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return JsonSerializer.Deserialize<ProductLaborPriceModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task UpdateAsync(ProductLaborPriceModel model)
        {
            var response = await ApiClient.Client.PutAsJsonAsync($"api/productlaborprices/{model.Id}", model);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
        }

        public async Task DeleteAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/productlaborprices/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
        }
    }
}