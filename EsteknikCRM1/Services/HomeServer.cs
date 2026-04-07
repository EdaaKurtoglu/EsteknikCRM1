using Google.Cloud.Firestore;
using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class HomeService
    {
        private readonly FirestoreDb _db;

        public HomeService(FirestoreDb db)
        {
            _db = db;
        }

        public async Task<List<HomePageModel>> GetHomeTextsAsync()
        {
            Query query = _db.Collection("HomePage").OrderBy("date");
            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<HomePageModel> list = new List<HomePageModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (!doc.Exists) continue;

                list.Add(new HomePageModel
                {
                    Id = doc.Id,
                    HomeText = doc.GetValue<string>("homeText"),
                    Date = doc.GetValue<Timestamp>("date").ToDateTime()
                });
            }

            return list;
        }
    }
}