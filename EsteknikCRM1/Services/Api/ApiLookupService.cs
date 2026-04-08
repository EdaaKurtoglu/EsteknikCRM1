using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services.Api
{
    public class ApiLookupService
    {
        public async Task<List<CategoriesModel>> GetCategoriesAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<CategoriesModel>>("api/lookups/categories")
                   ?? new List<CategoriesModel>();
        }

        public async Task<List<NotificationTypeModel>> GetNotificationTypeAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<NotificationTypeModel>>("api/lookups/notificationtypes")
                   ?? new List<NotificationTypeModel>();
        }

        public async Task<List<SubCategoriesModel>> GetSubCategoriesAsync()
        {
            return await ApiClient.Client.GetFromJsonAsync<List<SubCategoriesModel>>("api/lookups/subcategories")
                   ?? new List<SubCategoriesModel>();
        }
    }
}