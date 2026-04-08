using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiTeamService
    {
        public async Task<List<TeamItem>> GetTeamsAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<TeamItem>>("api/teams")
                   ?? new List<TeamItem>();
        }

        public async Task AddTeamAsync(TeamItem team)
        {
            var response = await ApiClient.Client.PostAsJsonAsync("api/teams", team);
            response.EnsureSuccessStatusCode();
        }
    }
}