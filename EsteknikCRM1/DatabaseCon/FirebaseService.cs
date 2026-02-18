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
    }
}
