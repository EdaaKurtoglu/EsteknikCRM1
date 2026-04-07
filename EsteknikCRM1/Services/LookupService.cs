using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class LookupService
    {
        private readonly FirestoreDb db;

        public LookupService(FirestoreDb _db)
        {
            db = _db;
        }
        public async Task<List<CategoriesModel>> GetCategoriesAsync()
        {
            Query query = db.Collection("Categories");

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<CategoriesModel> categories = new List<CategoriesModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    categories.Add(new CategoriesModel
                    {
                        ID = doc.Id,
                        Category = doc.GetValue<string>("CategoryName")
                    });
                }
            }

            return categories;
        }
        public async Task<List<NotificationTypeModel>> GetNotificationTypeAsync()
        {
            Query query = db.Collection("NotificationTypes");

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<NotificationTypeModel> notifications = new List<NotificationTypeModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    notifications.Add(new NotificationTypeModel
                    {
                        ID = doc.Id,
                        NotificationType = doc.GetValue<string>("NotifyType")
                    });
                }
            }

            return notifications;
        }
        public async Task<List<SubCategoriesModel>> GetSubCategoriesAsync()
        {
            Query query = db.Collection("SubCategories");

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<SubCategoriesModel> subcategories = new List<SubCategoriesModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    subcategories.Add(new SubCategoriesModel
                    {
                        ID = doc.Id,
                        Name = doc.GetValue<string>("Name")
                    });
                }
            }

            return subcategories;
        }

    }
}