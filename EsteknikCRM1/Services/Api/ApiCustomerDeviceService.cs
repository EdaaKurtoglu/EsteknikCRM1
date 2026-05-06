using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiCustomerDeviceService
    {
        public async Task<List<DeviceModel>> GetDevicesByCustomerIdAsync(string customerId)
        {
            return await ApiClient.Client.GetFromJsonAsync<List<DeviceModel>>($"api/customerdevice/customer/{customerId}")
                   ?? new List<DeviceModel>();
        }

        public async Task AddCustomerDeviceRelationAsync(string customerId, string deviceId)
        {
            var payload = new
            {
                Id = Guid.NewGuid().ToString(),

                CustomerId = customerId,
                DeviceId = deviceId,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                Status = "active"
            };

            var response = await ApiClient.Client.PostAsJsonAsync("api/customerdevice", payload);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");
        }
    }
}