using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiCustomerService
    {
        public async Task<List<CustomerModel>> GetCustomersAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<CustomerModel>>("api/customer")
                   ?? new List<CustomerModel>();
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(string id)
        {
            var response = await ApiClient.Client.GetAsync($"api/customer/{id}");

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
            }

            return System.Text.Json.JsonSerializer.Deserialize<CustomerModel>(content, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        public async Task<string> AddCustomerAsync(CustomerModel customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            var response = await ApiClient.Client.PostAsJsonAsync("api/customer", customer);
            response.EnsureSuccessStatusCode();

            var createdCustomer = await response.Content.ReadFromJsonAsync<CustomerModel>();
            return createdCustomer?.Id;
        }

        public async Task UpdateCustomerAsync(string id, CustomerModel customer)
        {
            var response = await ApiClient.Client.PutAsJsonAsync($"api/customer/{id}", customer);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomerAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/customer/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}