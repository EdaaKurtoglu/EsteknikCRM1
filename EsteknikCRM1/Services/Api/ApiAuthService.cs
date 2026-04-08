using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiAuthService
    {
        public async Task<List<UserModel>> GetUsersAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<UserModel>>("api/auth/users")
                   ?? new List<UserModel>();
        }
    }
}