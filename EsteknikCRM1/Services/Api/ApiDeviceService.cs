using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiDeviceService
    {
        public async Task<List<DeviceModel>> GetDevicesAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<DeviceModel>>("api/device")
                   ?? new List<DeviceModel>();
        }

        public async Task<DeviceModel> GetDeviceByIdAsync(string id)
        {
            var response = await ApiClient.Client.GetAsync($"api/device/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
            }

            return JsonSerializer.Deserialize<DeviceModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        public async Task<string> AddDeviceAsync(DeviceModel device)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/device", device);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");

            var createdDevice = await response.Content.ReadFromJsonAsync<DeviceModel>();
            return createdDevice?.Id ?? string.Empty;
        }
    }
}