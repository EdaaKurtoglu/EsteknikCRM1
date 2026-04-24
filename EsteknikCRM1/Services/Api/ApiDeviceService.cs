using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiDeviceService
    {
        public async Task<List<DeviceModel>> GetDevicesAsync()
        {
            var response = await ApiClient.Client.GetAsync("api/device");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n\n{content}");

            return System.Text.Json.JsonSerializer.Deserialize<List<DeviceModel>>(content,
                new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<DeviceModel>();
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
            device.Id = Guid.NewGuid().ToString();
            var response = await ApiClient.Client.PostAsJsonAsync("api/device", device);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"API Hatası: {response.StatusCode}\n{responseContent}");

            var createdDevice = await response.Content.ReadFromJsonAsync<DeviceModel>();
            return createdDevice?.Id ?? string.Empty;
        }
        public async Task<DeviceModel> GetDeviceBySerialOrStockCodeAsync(string serialNo, string stockCode)
        {
            string url =
                $"api/device/search?serialNo={Uri.EscapeDataString(serialNo ?? "")}&stockCode={Uri.EscapeDataString(stockCode ?? "")}";

            var response = await ApiClient.Client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            return JsonSerializer.Deserialize<DeviceModel>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}