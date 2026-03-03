using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.DatabaseCon
{
    internal class FirebaseService
    {
        private FirestoreDb db;

        public FirebaseService()
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "serviceAccountKey.json");
            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("esteknikcrm"); // 🔥 Burayı doldur


        }

        public async Task AddUserAsync(string usermail, string password)
        {
            CollectionReference usersRef = db.Collection("Users");

            Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "usermail", usermail},
            { "password", password }
        };

            await usersRef.AddAsync(user);
        }

        public async Task<bool> CheckUserAsync(string usermail, string password, string userRole)
        {
            Query query = db.Collection("Users")
                            .WhereEqualTo("usermail", usermail)
                            .WhereEqualTo("password", password)
                            .WhereEqualTo("userRole", userRole);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            return snapshot.Count > 0;
        }

        public async Task<UserModel> LoginAsync(string usermail, string password, string userRole)
        {
            Query query = db.Collection("Users")
                            .WhereEqualTo("usermail", usermail)
                            .WhereEqualTo("password", password)
                            .WhereEqualTo("userRole", userRole);
            

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count == 0)
                return null;

            var doc = snapshot.Documents[0];

            return new UserModel
            {
                UserMail = doc.GetValue<string>("usermail"),
                Name = doc.GetValue<string>("name"),
                Surname = doc.GetValue<string>("surname"),
                UserRole = doc.GetValue<string>("userRole")
            };
        }

        //HomePage Tablosu
        public async Task<List<HomePageModel>> GetHomeTextsAsync()
        {
            Query query = db.Collection("HomePage")
                            .OrderBy("date"); // tarihe göre sırala

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<HomePageModel> list = new List<HomePageModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    list.Add(new HomePageModel
                    {
                        Id = doc.Id,
                        HomeText = doc.GetValue<string>("homeText"),
                        Date = doc.GetValue<Timestamp>("date").ToDateTime()
                    });
                }
            }

            return list;
        }

    }
}
