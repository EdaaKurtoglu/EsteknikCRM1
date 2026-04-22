using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiProductService
    {
        public async Task<List<ProductModel>> GetProductsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<ProductModel>>("api/products")
                   ?? new List<ProductModel>();
        }

        public async Task<string> AddProductAsync(ProductModel product)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/products", product);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            var created = JsonSerializer.Deserialize<ProductModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return created?.Id ?? string.Empty;
        }
    }
}