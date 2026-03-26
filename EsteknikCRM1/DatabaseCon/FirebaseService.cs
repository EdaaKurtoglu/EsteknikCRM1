using Google.Cloud.Firestore;
using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

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

        /*public async Task<List<CustomerModel>> GetCustomersAsync()
        {
            QuerySnapshot snapshot = await db.Collection("Customers").GetSnapshotAsync();
            List<CustomerModel> customers = new List<CustomerModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                var data = doc.ToDictionary();

                customers.Add(new CustomerModel
                {
                    Id = doc.Id,
                    Name = data.ContainsKey("Name") ? data["Name"]?.ToString() : "",
                    Surname = data.ContainsKey("Surname") ? data["Surname"]?.ToString() : "",
                    Phone = data.ContainsKey("Phone") ? data["Phone"]?.ToString() : "",
                    MobilePhone = data.ContainsKey("MobilePhone") ? data["MobilePhone"]?.ToString() : "",
                    City = data.ContainsKey("City") ? data["City"]?.ToString() : "",
                    Address = data.ContainsKey("Address") ? data["Address"]?.ToString() : ""
                });
            }

            return customers;
        }*/

        public async Task<List<CustomerModel>> GetCustomersAsync()
        {
            QuerySnapshot snapshot = await db.Collection("Customers").GetSnapshotAsync();
            List<CustomerModel> customers = new List<CustomerModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                Dictionary<string, object> data = doc.ToDictionary();

                customers.Add(new CustomerModel
                {
                    Id = doc.Id,
                    CustomerNo = data.ContainsKey("CustomerNo") ? data["CustomerNo"]?.ToString() : "",
                    Name = data.ContainsKey("Name") ? data["Name"]?.ToString() : "",
                    MiddleName = data.ContainsKey("MiddleName") ? data["MiddleName"]?.ToString() : "",
                    Surname = data.ContainsKey("Surname") ? data["Surname"]?.ToString() : "",

                    Phone = data.ContainsKey("Phone") ? data["Phone"]?.ToString() : "",
                    MobilePhone = data.ContainsKey("MobilePhone") ? data["MobilePhone"]?.ToString() : "",

                    Email = data.ContainsKey("Email") ? data["Email"]?.ToString() : "",
                    Address = data.ContainsKey("Address") ? data["Address"]?.ToString() : "",

                    Status = data.ContainsKey("Status") ? data["Status"]?.ToString() : "Aktif"
                });
            }

            return customers;
        }

        public async Task<string> AddCustomerAddressAsync(AddressModel address)
        {
            CollectionReference addressRef = db.Collection("Addresses");

            Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "CustomerId", address.CustomerId ?? "" },
                    { "AddressLine", address.AddressLine ?? "" },

                    { "Country", address.Country ?? "" },
                    { "City", address.City ?? "" },
                    { "District", address.District ?? "" },
                    { "Neighborhood", address.Neighborhood ?? "" },
                    { "Street", address.Street ?? "" },
                    { "PostCode", address.PostCode ?? "" },
                    { "BuildingNo", address.BuildingNo ?? "" },
                    { "FlatNo", address.FlatNo ?? "" },

                    { "IsActive", address.IsActive },
                    { "IsResidence", address.IsResidence },
                    { "OwnershipType", address.OwnershipType ?? "" },
                    { "Status", address.Status ?? "Aktif" },
                    { "CreatedDate", address.CreatedDate.HasValue ? address.CreatedDate.Value : (object)"" },
                    { "PassiveDate", address.PassiveDate.HasValue ? address.PassiveDate.Value : (object)"" }
                };

            DocumentReference addedDoc = await addressRef.AddAsync(data);
            return addedDoc.Id;
        }
        public async Task<List<AddressModel>> GetCustomerAddressesAsync(string customerId)
        {
            Query query = db.Collection("Addresses")
                            .WhereEqualTo("CustomerId", customerId);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            List<AddressModel> addresses = new List<AddressModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    var data = doc.ToDictionary();

                    addresses.Add(new AddressModel
                    {
                        Id = doc.Id,
                        CustomerId = data.ContainsKey("CustomerId") ? data["CustomerId"]?.ToString() : "",
                        AddressLine = data.ContainsKey("AddressLine") ? data["AddressLine"]?.ToString() : "",

                        Country = data.ContainsKey("Country") ? data["Country"]?.ToString() : "",
                        City = data.ContainsKey("City") ? data["City"]?.ToString() : "",
                        District = data.ContainsKey("District") ? data["District"]?.ToString() : "",
                        Neighborhood = data.ContainsKey("Neighborhood") ? data["Neighborhood"]?.ToString() : "",
                        Street = data.ContainsKey("Street") ? data["Street"]?.ToString() : "",
                        PostCode = data.ContainsKey("PostCode") ? data["PostCode"]?.ToString() : "",
                        BuildingNo = data.ContainsKey("BuildingNo") ? data["BuildingNo"]?.ToString() : "",
                        FlatNo = data.ContainsKey("FlatNo") ? data["FlatNo"]?.ToString() : "",

                        OwnershipType = data.ContainsKey("OwnershipType") ? data["OwnershipType"]?.ToString() : "",
                        Status = data.ContainsKey("Status") ? data["Status"]?.ToString() : "Aktif",
                        IsActive = data.ContainsKey("IsActive") && data["IsActive"] is bool active && active,
                        IsResidence = data.ContainsKey("IsResidence") && data["IsResidence"] is bool residence && residence
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

        public async Task<string> AddWorkflowAsync(WorkflowModel workflow)
        {
            CollectionReference workflowsRef = db.Collection("Workflows");

            Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "StartType", workflow.StartType ?? "" },

                    { "CustomerId", workflow.CustomerId ?? "" },
                    { "CustomerName", workflow.CustomerName ?? "" },
                    { "CustomerSurname", workflow.CustomerSurname ?? "" },
                    { "CustomerFullName", workflow.CustomerFullName ?? "" },

                    { "AddressId", workflow.AddressId ?? "" },
                    { "AddressLine", workflow.AddressLine ?? "" },

                    { "CategoryId", workflow.CategoryId ?? "" },
                    { "CategoryName", workflow.CategoryName ?? "" },

                    { "NotificationTypeId", workflow.NotificationTypeId ?? "" },
                    { "NotificationTypeName", workflow.NotificationTypeName ?? "" },

                    { "SubCategoryId", workflow.SubCategoryId ?? "" },
                    { "SubCategoryName", workflow.SubCategoryName ?? "" },

                    { "DeviceId", workflow.DeviceId ?? "" },
                    { "DeviceName", workflow.DeviceName ?? "" },

                    { "Description", workflow.Description ?? "" },
                    { "ExtraDescription", workflow.ExtraDescription ?? "" },
                    { "ArrivalChannel", workflow.ArrivalChannel ?? "" },

                    { "WorkflowStatus", workflow.WorkflowStatus ?? "Devam Ediyor" },
                    { "FlowType", workflow.FlowType ?? "" },
                    { "Subject", workflow.Subject ?? "" },

                    { "CreatedByUserMail", workflow.CreatedByUserMail ?? "" },
                    { "CreatedByName", workflow.CreatedByName ?? "" },
                    { "CreatedBySurname", workflow.CreatedBySurname ?? "" },
                    { "CreatedByRole", workflow.CreatedByRole ?? "" },

                    // Firestore için UTC güvenli
                    { "CreatedDate", Timestamp.FromDateTime(workflow.CreatedDate.ToUniversalTime()) }
                };

            DocumentReference addedDoc = await workflowsRef.AddAsync(data);
            return addedDoc.Id;
        }

        public async Task<List<WorkflowModel>> GetWorkflowsAsync()
        {
            QuerySnapshot snapshot = await db.Collection("Workflows")
                                             .OrderByDescending("CreatedDate")
                                             .GetSnapshotAsync();

            List<WorkflowModel> workflows = new List<WorkflowModel>();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                Dictionary<string, object> data = doc.ToDictionary();

                DateTime createdDate = DateTime.MinValue;
                if (data.ContainsKey("CreatedDate") && data["CreatedDate"] is Timestamp ts)
                {
                    createdDate = ts.ToDateTime().ToLocalTime();
                }

                workflows.Add(new WorkflowModel
                {
                    Id = doc.Id,
                    StartType = data.ContainsKey("StartType") ? data["StartType"]?.ToString() : "",

                    CustomerId = data.ContainsKey("CustomerId") ? data["CustomerId"]?.ToString() : "",
                    CustomerName = data.ContainsKey("CustomerName") ? data["CustomerName"]?.ToString() : "",
                    CustomerSurname = data.ContainsKey("CustomerSurname") ? data["CustomerSurname"]?.ToString() : "",
                    CustomerFullName = data.ContainsKey("CustomerFullName") ? data["CustomerFullName"]?.ToString() : "",

                    AddressId = data.ContainsKey("AddressId") ? data["AddressId"]?.ToString() : "",
                    AddressLine = data.ContainsKey("AddressLine") ? data["AddressLine"]?.ToString() : "",

                    CategoryId = data.ContainsKey("CategoryId") ? data["CategoryId"]?.ToString() : "",
                    CategoryName = data.ContainsKey("CategoryName") ? data["CategoryName"]?.ToString() : "",

                    NotificationTypeId = data.ContainsKey("NotificationTypeId") ? data["NotificationTypeId"]?.ToString() : "",
                    NotificationTypeName = data.ContainsKey("NotificationTypeName") ? data["NotificationTypeName"]?.ToString() : "",

                    SubCategoryId = data.ContainsKey("SubCategoryId") ? data["SubCategoryId"]?.ToString() : "",
                    SubCategoryName = data.ContainsKey("SubCategoryName") ? data["SubCategoryName"]?.ToString() : "",

                    DeviceId = data.ContainsKey("DeviceId") ? data["DeviceId"]?.ToString() : "",
                    DeviceName = data.ContainsKey("DeviceName") ? data["DeviceName"]?.ToString() : "",

                    Description = data.ContainsKey("Description") ? data["Description"]?.ToString() : "",
                    ExtraDescription = data.ContainsKey("ExtraDescription") ? data["ExtraDescription"]?.ToString() : "",
                    ArrivalChannel = data.ContainsKey("ArrivalChannel") ? data["ArrivalChannel"]?.ToString() : "",

                    WorkflowStatus = data.ContainsKey("WorkflowStatus") ? data["WorkflowStatus"]?.ToString() : "",
                    FlowType = data.ContainsKey("FlowType") ? data["FlowType"]?.ToString() : "",
                    Subject = data.ContainsKey("Subject") ? data["Subject"]?.ToString() : "",

                    CreatedByUserMail = data.ContainsKey("CreatedByUserMail") ? data["CreatedByUserMail"]?.ToString() : "",
                    CreatedByName = data.ContainsKey("CreatedByName") ? data["CreatedByName"]?.ToString() : "",
                    CreatedBySurname = data.ContainsKey("CreatedBySurname") ? data["CreatedBySurname"]?.ToString() : "",
                    CreatedByRole = data.ContainsKey("CreatedByRole") ? data["CreatedByRole"]?.ToString() : "",

                    CreatedDate = createdDate
                });
            }

            return workflows;
        }

        public async Task<string> AddCustomerAsync(CustomerModel customer)
        {
            CollectionReference customersRef = db.Collection("Customers");

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "IsVip", customer.IsVip },
                { "CustomerNo", customer.CustomerNo ?? "" },
                { "Name", customer.Name ?? "" },
                { "MiddleName", customer.MiddleName ?? "" },
                { "Surname", customer.Surname ?? "" },

                { "Phone", customer.Phone ?? "" },
                { "MobilePhone", customer.MobilePhone ?? "" },
                { "Phone2", customer.Phone2 ?? "" },
                { "MobilePhone2", customer.MobilePhone2 ?? "" },

                { "UnknownEmail", customer.UnknownEmail },
                { "Email", customer.Email ?? "" },
                { "Email2", customer.Email2 ?? "" },

                { "Description", customer.Description ?? "" },

                { "Country", customer.Country ?? "" },
                { "City", customer.City ?? "" },
                { "District", customer.District ?? "" },
                { "Neighborhood", customer.Neighborhood ?? "" },
                { "Address", customer.Address ?? "" },

                { "SpecialProjectInfo", customer.SpecialProjectInfo ?? "" },
                { "BuildingInfo", customer.BuildingInfo ?? "" },

                { "Status", customer.Status ?? "Aktif" }
            };

                    DocumentReference addedDoc = await customersRef.AddAsync(data);
                    return addedDoc.Id;
        }

        
    }


}
