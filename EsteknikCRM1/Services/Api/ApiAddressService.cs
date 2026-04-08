using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiAddressService
    {
        public async Task<List<AddressModel>> GetAddressesByCustomerIdAsync(string customerId)
        {
            return await ApiClient.Client.GetFromJsonAsync<List<AddressModel>>($"api/address/customer/{customerId}")
                   ?? new List<AddressModel>();
        }

        public async Task<AddressModel> GetAddressByIdAsync(string id)
        {
            return await ApiClient.Client.GetFromJsonAsync<AddressModel>($"api/address/{id}");
        }

        public async Task AddAddressAsync(AddressModel address)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/address", address);

            var errorContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{errorContent}");
            }
        }

        public async Task DeleteAddressAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/address/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}