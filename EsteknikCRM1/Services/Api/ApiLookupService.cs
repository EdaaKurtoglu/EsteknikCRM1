using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Net.Http;
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
        public async Task<List<SubCategoriesModel>> GetSubCategoriesByCategoryIdAsync(string categoryId)
        {
            return await ApiClient.Client.GetFromJsonAsync<List<SubCategoriesModel>>(
                $"api/lookups/subcategories/by-category/{categoryId}")
                ?? new List<SubCategoriesModel>();
        }
        public async Task<List<NotificationTypeModel>> GetNotificationTypesByCategoryIdAsync(string categoryId)
        {
            return await ApiClient.Client.GetFromJsonAsync<List<NotificationTypeModel>>(
                $"api/lookups/notificationtypes/by-category/{categoryId}")
                ?? new List<NotificationTypeModel>();
        }

        public async Task<List<SubCategoriesModel>> GetSubCategoriesByCategoryAndNotificationTypeAsync(
            string categoryId,
            string notificationTypeId)
        {
            return await ApiClient.Client.GetFromJsonAsync<List<SubCategoriesModel>>(
                $"api/lookups/subcategories/by-category-and-notification?categoryId={categoryId}&notificationTypeId={notificationTypeId}")
                ?? new List<SubCategoriesModel>();
        }
    }
}