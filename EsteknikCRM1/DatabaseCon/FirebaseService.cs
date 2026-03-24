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
    public class FirebaseService
    {
        private static FirebaseService _instance;
        private static readonly object _lock = new object();
        private FirestoreDb db;

        private FirebaseService()
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "serviceAccountKey.json");
            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS", path);

            db = FirestoreDb.Create("esteknikcrm"); // 🔥 Burayı doldur


        }
        public static FirebaseService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new FirebaseService();

                    return _instance;
                }
            }
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

        public async Task<List<CustomerModel>> GetCustomersAsync()
        {
            Query query = db.Collection("Customers");

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<CustomerModel> customers = new List<CustomerModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    customers.Add(new CustomerModel
                    {
                        Id = doc.Id,
                        Name = doc.ContainsField("CustomerName") ? doc.GetValue<string>("CustomerName") : "",
                        Surname = doc.ContainsField("CustomerSurname") ? doc.GetValue<string>("CustomerSurname") : "",
                        Phone = doc.ContainsField("MobilPhone") ? doc.GetValue<string>("MobilPhone") : "",
                        Adress = doc.ContainsField("Adress") ? doc.GetValue<string>("Adress") : ""
                    });
                }
            }

            return customers;
        }

        public async Task<List<AddressModel>> GetCustomerAddressesAsync(string customerId)
        {
            Query query = db.Collection("Adresses")
                            .WhereEqualTo("CustomerId", customerId);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<AddressModel> addresses = new List<AddressModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    addresses.Add(new AddressModel
                    {
                        Id = doc.Id,
                        CustomerId = doc.GetValue<string>("CustomerId"),
                        AddressLine = doc.GetValue<string>("AdressLine")
                    });
                }
            }

            return addresses;
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

        public async Task<List<DeviceModel>> GetDevicesAsync()
        {
            Query query = db.Collection("Devices");

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<DeviceModel> devices = new List<DeviceModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    devices.Add(new DeviceModel
                    {
                        Id = doc.Id,
                        DeviceName = doc.GetValue<string>("DeviceName")
                    });
                }
            }

            return devices;
        }
    }


}
