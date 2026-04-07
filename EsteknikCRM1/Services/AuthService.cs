using Google.Cloud.Firestore;
using EsteknikCRM1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class AuthService
    {
        private readonly FirestoreDb _db;

        public AuthService(FirestoreDb db)
        {
            _db = db;
        }

        public async Task AddUserAsync(string usermail, string password, string userRole, string name, string surname)
        {
            CollectionReference usersRef = _db.Collection("Users");

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "usermail", usermail },
                { "password", password },
                { "userRole", userRole },
                { "name", name },
                { "surname", surname }
            };

            await usersRef.AddAsync(user);
        }

        public async Task<bool> CheckUserAsync(string usermail, string password, string userRole)
        {
            Query query = _db.Collection("Users")
                .WhereEqualTo("usermail", usermail)
                .WhereEqualTo("password", password)
                .WhereEqualTo("userRole", userRole);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            return snapshot.Count > 0;
        }

        public async Task<UserModel> LoginAsync(string usermail, string password, string userRole)
        {
            Query query = _db.Collection("Users")
                .WhereEqualTo("usermail", usermail)
                .WhereEqualTo("password", password)
                .WhereEqualTo("userRole", userRole);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count == 0)
                return null;

            var doc = snapshot.Documents[0];

            return new UserModel
            {
                Id = doc.Id,
                UserMail = doc.GetValue<string>("usermail"),
                Password = doc.GetValue<string>("password"),
                Name = doc.GetValue<string>("name"),
                Surname = doc.GetValue<string>("surname"),
                UserRole = doc.GetValue<string>("userRole")
            };
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            QuerySnapshot snapshot = await _db.Collection("Users").GetSnapshotAsync();
            List<UserModel> users = new List<UserModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (!doc.Exists) continue;

                users.Add(new UserModel
                {
                    Id = doc.Id,
                    Name = doc.ContainsField("Name") ? doc.GetValue<string>("Name") : "",
                    Surname = doc.ContainsField("Surname") ? doc.GetValue<string>("Surname") : "",
                    UserRole = doc.ContainsField("UserRole") ? doc.GetValue<string>("UserRole") : "",
                    Department = doc.ContainsField("Department") ? doc.GetValue<string>("Department") : ""
                });
            }

            return users;
        }
    }
}