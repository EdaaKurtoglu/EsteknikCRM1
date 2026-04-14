using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiTeamService
    {
        public async Task<List<TeamItem>> GetTeamsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<TeamItem>>("api/team")
                   ?? new List<TeamItem>();
        }

        public async Task<string> AddTeamAsync(TeamItem team)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/team", team);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");

            var createdTeam = JsonSerializer.Deserialize<TeamItem>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return createdTeam?.Id ?? createdTeam?.TeamId ?? string.Empty;
        }

        public async Task UpdateTeamAsync(string id, TeamItem team)
        {
            var response = await ApiClient.Client.PutAsJsonAsync($"api/team/{id}", team);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
        }

        public async Task DeleteTeamAsync(string id)
        {
            var response = await ApiClient.Client.DeleteAsync($"api/team/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API Hatası: {response.StatusCode}\n{content}");
        }
    }
}