using Google.Cloud.Firestore;
using EsteknikCRM1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Firebase.Storage;
using EsteknikCRM1.Services;

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

        public async Task AddUserAsync(string usermail, string password, string userRole, string name, string surname)
        {
            CollectionReference usersRef = db.Collection("Users");

            Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "usermail", usermail},
            { "password", password },
            {"userRole" ,userRole},
            {"name" ,name},
            {"surname" ,surname},

        };

            await usersRef.AddAsync(user);
        }

        public async Task<bool> CheckUserAsync(string usermail, string password, string userRole)
        {
            Query query = db.Collection("Teams")
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
                Password = doc.GetValue<string>("password"),
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

        /*public async Task<List<DeviceModel>> GetDevicesAsync()
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
        */
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
                    WorkTeam = data.ContainsKey("Workteam") ? data["Workteam"]?.ToString() : "",


                    CreatedDate = createdDate
                });
            }

            return workflows;
        }
        public async Task UpdateWorkflowStatusAsync(string workflowId, string newStatus)
        {
            try
            {
                DocumentReference docRef = db.Collection("Workflows").Document(workflowId);

                Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "WorkflowStatus", newStatus }
        };

                await docRef.UpdateAsync(updates);
            }
            catch (Exception ex)
            {
                throw new Exception("Workflow durumu güncellenemedi: " + ex.Message);
            }
        }
        public async Task UpdateWorkflowTeamAsync(string workflowId, string team)
        {
            try
            {
                DocumentReference docRef = db.Collection("Workflows").Document(workflowId);

                Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "Workteam", team }
        };

                await docRef.UpdateAsync(updates);
            }
            catch (Exception ex)
            {
                throw new Exception("Workflow durumu güncellenemedi: " + ex.Message);
            }
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

        public async Task<string> AddTeamAsync(TeamItem team)
        {
            try
            {
                CollectionReference teamsRef = db.Collection("Teams");

                Dictionary<string, object> data = new Dictionary<string, object>
                {
                    { "TeamName", team.TeamName ?? string.Empty },
                    { "VehiclePlate", team.VehiclePlate ?? string.Empty },
                    { "IsActive", team.IsActive },
                    { "Status", team.Status ?? "Aktif" },
                    { "CreatedDate", team.CreatedDate.ToUniversalTime() },
                    { "PassiveDate", team.PassiveDate.HasValue ? (object)team.PassiveDate.Value.ToUniversalTime() : null },
                    { "ServiceName", team.ServiceName ?? string.Empty
                    }
                };

                DocumentReference addedDoc = await teamsRef.AddAsync(data);
                return addedDoc.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Takım kaydedilirken hata oluştu: " + ex.Message);
            }

        }

        public async Task<List<TeamItem>> GetTeamsAsync()
        {
            try
            {
                QuerySnapshot snapshot = await db.Collection("Teams").GetSnapshotAsync();
                List<TeamItem> teams = new List<TeamItem>();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (!doc.Exists)
                        continue;

                    string serviceName = "";
                    string teamName = "";
                    int memberCount = 0;
                    string memberName = "";
                    string role = "";
                    string status = "";
                    string teamId = "";

                    if (doc.ContainsField("ServiceName"))
                        serviceName = doc.GetValue<string>("ServiceName");

                    if (doc.ContainsField("TeamName"))
                        teamName = doc.GetValue<string>("TeamName");

                    if (doc.ContainsField("MemberCount"))
                        memberCount = doc.GetValue<int>("MemberCount");

                    if (doc.ContainsField("MemberName"))
                        memberName = doc.GetValue<string>("MemberName");

                    if (doc.ContainsField("Role"))
                        role = doc.GetValue<string>("Role");

                    if (doc.ContainsField("Status"))
                        status = doc.GetValue<string>("Status");
                    int rowNo = 1;

                    teams.Add(new TeamItem
                    {
                        TeamId = teamId,
                        ServiceName = serviceName,
                        TeamName = teamName,
                        MemberCount = memberCount,
                        MemberName = memberName,
                        Role = role,
                        Status = status
                    });
                    rowNo++;
                }

                return teams;
            }
            catch (Exception ex)
            {
                throw new Exception("Takımlar alınırken hata oluştu: " + ex.Message);
            }
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(string customerId)
        {
            try
            {
                DocumentReference docRef = db.Collection("Customers").Document(customerId);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return null;

                return new CustomerModel
                {
                    Id = docRef.Id,
                    Name = snapshot.GetValue<string>("Name"),
                    Surname = snapshot.GetValue<string>("Surname"),
                    Phone = snapshot.ContainsField("Phone") ? snapshot.GetValue<string>("Phone") : ""
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri alınamadı: " + ex.Message);
            }
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                QuerySnapshot snapshot = await db.Collection("Users").GetSnapshotAsync();
                List<UserModel> users = new List<UserModel>();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (!doc.Exists)
                        continue;

                    string name = doc.ContainsField("Name") ? doc.GetValue<string>("Name") : "";
                    string surname = doc.ContainsField("Surname") ? doc.GetValue<string>("Surname") : "";
                    string role = doc.ContainsField("UserRole") ? doc.GetValue<string>("UserRole") : "";
                    string department = doc.ContainsField("Department") ? doc.GetValue<string>("Department") : "";

                    users.Add(new UserModel
                    {
                        Id = doc.Id,
                        Name = name,
                        Surname = surname,
                        UserRole = role,
                        Department = department
                    });
                }

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcılar alınırken hata oluştu: " + ex.Message);
            }
        }


        public async Task<DeviceModel> GetDeviceByIdAsync(string deviceId)
        {
            try
            {
                DocumentReference docRef = db.Collection("Devices").Document(deviceId);
                DocumentSnapshot doc = await docRef.GetSnapshotAsync();

                if (!doc.Exists)
                    return null;

                return new DeviceModel
                {
                    Id = doc.Id,

                    SerialNumber = doc.ContainsField("SerialNumber")
                        ? doc.GetValue<string>("SerialNumber")
                        : "",

                    DeviceName = doc.ContainsField("DeviceName")
                        ? doc.GetValue<string>("DeviceName")
                        : "",

                    Brand = doc.ContainsField("Brand")
                        ? doc.GetValue<string>("Brand")
                        : "",

                    TopGroup = doc.ContainsField("TopGroup")
                        ? doc.GetValue<string>("TopGroup")
                        : "",

                    CommissionDate = doc.ContainsField("CommissionDate")
                        ? doc.GetValue<Timestamp>("CommissionDate").ToDateTime()
                        : (DateTime?)null,

                    DeviceCode = doc.ContainsField("DeviceCode")
                         ? doc.GetValue<string>("DeviceCode")
                        : "",

                    SubGroup = doc.ContainsField("SubGroup")
                        ? doc.GetValue<string>("SubGroup")
                        : "",

                    SpecialGroup = doc.ContainsField("SpecialGroup")
                        ? doc.GetValue<string>("SpecialGroup")
                        : "",


                };
            }
            catch (Exception ex)
            {
                throw new Exception("Cihaz bilgisi alınamadı: " + ex.Message);
            }
        }

        public async Task<string> AddDeviceAsync(DeviceModel device)
        {
            try
            {
                CollectionReference devicesRef = db.Collection("Devices");

                Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "SerialNumber", device.SerialNumber ?? "" },
            { "DeviceCode", device.DeviceCode ?? "" },
            { "DeviceName", device.DeviceName ?? "" },
            { "Brand", device.Brand ?? "" },
            { "TopGroup", device.TopGroup ?? "" },
            { "SubGroup", device.SubGroup ?? "" },
            { "SpecialGroup", device.SpecialGroup ?? "" },
            { "Status", device.Status ?? "Aktif" }
        };

                if (device.CommissionDate.HasValue)
                {
                    data.Add("CommissionDate",
                        Google.Cloud.Firestore.Timestamp.FromDateTime(
                            DateTime.SpecifyKind(device.CommissionDate.Value, DateTimeKind.Local).ToUniversalTime()
                        ));
                }
                else
                {
                    data.Add("CommissionDate", null);
                }

                DocumentReference addedDoc = await devicesRef.AddAsync(data);
                return addedDoc.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Cihaz kaydedilirken hata oluştu: " + ex.Message);
            }
        }
        public async Task<List<DeviceModel>> GetDevicesAsync()
        {
            try
            {
                QuerySnapshot snapshot = await db.Collection("Devices").GetSnapshotAsync();
                List<DeviceModel> devices = new List<DeviceModel>();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (!doc.Exists)
                        continue;

                    devices.Add(new DeviceModel
                    {
                        Id = doc.Id,
                        SerialNumber = doc.ContainsField("SerialNumber") ? doc.GetValue<string>("SerialNumber") : "",
                        DeviceCode = doc.ContainsField("DeviceCode") ? doc.GetValue<string>("DeviceCode") : "",
                        DeviceName = doc.ContainsField("DeviceName") ? doc.GetValue<string>("DeviceName") : "",
                        CommissionDate = doc.ContainsField("CommissionDate")? doc.GetValue<Timestamp>("CommissionDate").ToDateTime():(DateTime?)null,
                        Brand = doc.ContainsField("Brand") ? doc.GetValue<string>("Brand") : "",
                        TopGroup = doc.ContainsField("TopGroup") ? doc.GetValue<string>("TopGroup") : "",
                        SubGroup = doc.ContainsField("SubGroup") ? doc.GetValue<string>("SubGroup") : "",
                        SpecialGroup = doc.ContainsField("SpecialGroup") ? doc.GetValue<string>("SpecialGroup") : ""
                    });
                }

                return devices;
            }
            catch (Exception ex)
            {
                throw new Exception("Cihazlar alınamadı: " + ex.Message);
            }
        }

        public async Task<List<WorkflowModel>> GetCompletedWorkflowsAsync()
        {
            try
            {
                QuerySnapshot snapshot = await db.Collection("Workflows")
                    .WhereEqualTo("WorkflowStatus", "Tamamlandı")
                    .GetSnapshotAsync();

                List<WorkflowModel> workflows = new List<WorkflowModel>();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (!doc.Exists)
                        continue;

                    workflows.Add(new WorkflowModel
                    {
                        Id = doc.Id,
                        WorkflowStatus = doc.ContainsField("WorkflowStatus") ? doc.GetValue<string>("WorkflowStatus") : "",
                        FlowType = doc.ContainsField("FlowType") ? doc.GetValue<string>("FlowType") : "",
                        CustomerId = doc.ContainsField("CustomerId") ? doc.GetValue<string>("CustomerId") : "",
                        DeviceId = doc.ContainsField("DeviceId") ? doc.GetValue<string>("DeviceId") : "",
                        CategoryName = doc.ContainsField("CategoryName") ? doc.GetValue<string>("CategoryName") : "",
                        SubCategoryName = doc.ContainsField("SubCategoryName") ? doc.GetValue<string>("SubCategoryName") : ""
                    });
                }

                return workflows;
            }
            catch (Exception ex)
            {
                throw new Exception("Tamamlanmış workflowlar alınamadı: " + ex.Message);
            }
        }

        public async Task<AddressModel> GetAddressByIdAsync(string addressId)
        {
            try
            {
                DocumentReference docRef = db.Collection("Adresses").Document(addressId);
                DocumentSnapshot doc = await docRef.GetSnapshotAsync();

                if (!doc.Exists)
                    return null;

                return new AddressModel
                {
                    Id = doc.Id,
                    CustomerId = doc.ContainsField("CustomerId") ? doc.GetValue<string>("CustomerId") : "",
                    AddressLine = doc.ContainsField("AdressLine") ? doc.GetValue<string>("AdressLine") : ""
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Adres alınamadı: " + ex.Message);
            }
        }
        public async Task<List<CustomerDeviceRelationModel>> GetCustomerDeviceRelationsAsync(string customerId)
        {
            try
            {
                QuerySnapshot snapshot = await db.Collection("CustomerDevices")
                    .WhereEqualTo("CustomerId", customerId)
                    .GetSnapshotAsync();

                List<CustomerDeviceRelationModel> relations = new List<CustomerDeviceRelationModel>();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (!doc.Exists)
                        continue;

                    relations.Add(new CustomerDeviceRelationModel
                    {
                        Id = doc.Id,
                        CustomerId = doc.ContainsField("CustomerId") ? doc.GetValue<string>("CustomerId") : "",
                        DeviceId = doc.ContainsField("DeviceId") ? doc.GetValue<string>("DeviceId") : "",
                        IsActive = doc.ContainsField("IsActive") && doc.GetValue<bool>("IsActive"),
                        CreatedDate = doc.ContainsField("CreatedDate")
                            ? doc.GetValue<Google.Cloud.Firestore.Timestamp>("CreatedDate").ToDateTime()
                            : DateTime.MinValue
                    });
                }

                return relations;
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri-cihaz ilişkileri alınamadı: " + ex.Message);
            }
        }
        public async Task<List<DeviceModel>> GetDevicesByCustomerIdAsync(string customerId)
        {
            try
            {
                var relations = await AppServices.CustomerDeviceService.GetCustomerDeviceRelationsAsync(customerId);
                //var relations = await GetCustomerDeviceRelationsAsync(customerId);
                var devices = new List<DeviceModel>();

                foreach (var relation in relations)
                {
                    if (string.IsNullOrWhiteSpace(relation.DeviceId))
                        continue;

                    var device = await GetDeviceByIdAsync(relation.DeviceId);
                    if (device != null)
                        devices.Add(device);
                }

                return devices;
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteriye ait cihazlar alınamadı: " + ex.Message);
            }
        }
        public async Task<string> AddCustomerDeviceRelationAsync(string customerId, string deviceId)
        {
            try
            {
                DocumentReference docRef = await db.Collection("CustomerDevices").AddAsync(new
                {
                    CustomerId = customerId,
                    DeviceId = deviceId,
                    IsActive = true,
                    CreatedDate = Google.Cloud.Firestore.Timestamp.FromDateTime(DateTime.UtcNow)
                });

                return docRef.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Müşteri-cihaz ilişkisi eklenemedi: " + ex.Message);
            }
        }
        public async Task<DeviceModel> GetDeviceBySerialOrStockCodeAsync(string serialNo, string stockCode)
        {
            try
            {
                QuerySnapshot snapshot = await db.Collection("Devices").GetSnapshotAsync();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (!doc.Exists)
                        continue;

                    string serial = doc.ContainsField("SerialNumber") ? doc.GetValue<string>("SerialNumber") : "";
                    string code = doc.ContainsField("DeviceCode") ? doc.GetValue<string>("DeviceCode") : "";

                    bool serialMatch = !string.IsNullOrWhiteSpace(serialNo) &&
                                       serial.Equals(serialNo, StringComparison.OrdinalIgnoreCase);

                    bool codeMatch = !string.IsNullOrWhiteSpace(stockCode) &&
                                     code.Equals(stockCode, StringComparison.OrdinalIgnoreCase);

                    if (serialMatch || codeMatch)
                    {
                        return new DeviceModel
                        {
                            Id = doc.Id,
                            SerialNumber = serial,
                            DeviceCode = code,
                            DeviceName = doc.ContainsField("DeviceName") ? doc.GetValue<string>("DeviceName") : "",
                            Brand = doc.ContainsField("Brand") ? doc.GetValue<string>("Brand") : "",
                            Status = doc.ContainsField("Status") ? doc.GetValue<string>("Status") : ""
                        };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Cihaz bulunamadı: " + ex.Message);
            }
        }
        public async Task<decimal> GetOperationPriceAsync(string stockCode, string operationType)
        {
            try
            {
                QuerySnapshot snapshot = await db.Collection("OperationPrices")
                    .WhereEqualTo("StockCode", stockCode)
                    .WhereEqualTo("OperationType", operationType)
                    .GetSnapshotAsync();

                if (snapshot.Documents.Count == 0)
                    return 0;

                var doc = snapshot.Documents[0];

                if (doc.ContainsField("Price"))
                {
                    object value = doc.GetValue<object>("Price");
                    return Convert.ToDecimal(value);
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Fiyat alınamadı: " + ex.Message);
            }
        }
        public async Task<string> AddWorkflowTeamOperationAsync(WorkflowTeamOperationSaveModel model)
        {
            try
            {
                CollectionReference collection = db.Collection("WorkflowTeamOperations");

                Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "WorkflowId", model.WorkflowId ?? "" },
            { "CustomerId", model.CustomerId ?? "" },
            { "DeviceId", model.DeviceId ?? "" },
            { "SerialNumber", model.SerialNumber ?? "" },
            { "StockCode", model.StockCode ?? "" },
            { "DeviceName", model.DeviceName ?? "" },
            { "OperationType", model.OperationType ?? "" },
            { "Price", model.Price },
            { "Quantity", model.Quantity },
            { "TotalAmount", model.TotalAmount },
            { "CreatedByUserMail", model.CreatedByUserMail ?? "" },
            { "CreatedByName", model.CreatedByName ?? "" },
            { "CreatedBySurname", model.CreatedBySurname ?? "" },
            { "CreatedByRole", model.CreatedByRole ?? "" },
            { "CreatedDate", Google.Cloud.Firestore.Timestamp.FromDateTime(model.CreatedDate.ToUniversalTime()) }
        };

                DocumentReference addedDoc = await collection.AddAsync(data);
                return addedDoc.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Workflow işlem kaydı oluşturulamadı: " + ex.Message);
            }
        }
        public async Task<List<RecordModel>> GetAnnouncementsAsync()
        {
            try
            {
                QuerySnapshot snapshot = await db.Collection("Announcements")
                    .OrderByDescending("CreatedDate")
                    .GetSnapshotAsync();

                List<RecordModel> announcements = new List<RecordModel>();

                foreach (DocumentSnapshot doc in snapshot.Documents)
                {
                    if (!doc.Exists)
                        continue;

                    DateTime createdDate = doc.ContainsField("CreatedDate")
                        ? doc.GetValue<Timestamp>("CreatedDate").ToDateTime()
                        : DateTime.Now;

                    var model = new RecordModel
                    {
                        Id = doc.Id,
                        Subject = doc.ContainsField("Subject") ? doc.GetValue<string>("Subject") : "",
                        BodyText = doc.ContainsField("BodyText") ? doc.GetValue<string>("BodyText") : "",
                        CreatedDate = createdDate,
                        DateText = createdDate.ToString("dd/MM/yyyy HH:mm"),
                        GroupText = GetGroupText(createdDate)
                    };

                    if (doc.ContainsField("FileNames"))
                        model.FileNames = doc.GetValue<List<string>>("FileNames");

                    if (doc.ContainsField("FileUrls"))
                        model.FileUrls = doc.GetValue<List<string>>("FileUrls");

                    if (doc.ContainsField("FileSizes"))
                        model.FileSizes = doc.GetValue<List<string>>("FileSizes");

                    announcements.Add(model);
                }

                return announcements;
            }
            catch (Exception ex)
            {
                throw new Exception("Duyurular alınamadı: " + ex.Message);
            }
        }

        private string GetGroupText(DateTime date)
        {
            int days = (DateTime.Now.Date - date.Date).Days;

            if (days <= 0) return "Bugün";
            if (days == 1) return "1 gün önce";
            return days + " gün önce";
        }
        public async Task<string> AddAnnouncementAsync(RecordModel model)
        {
            try
            {
                CollectionReference announcementsRef = db.Collection("Announcements");

                Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "Subject", model.Subject ?? "" },
            { "BodyText", model.BodyText ?? "" },
            { "CreatedDate", Timestamp.FromDateTime(model.CreatedDate.ToUniversalTime()) },
            { "FileNames", model.FileNames ?? new List<string>() },
            { "FileUrls", model.FileUrls ?? new List<string>() },
            { "FileSizes", model.FileSizes ?? new List<string>() }
        };

                DocumentReference docRef = await announcementsRef.AddAsync(data);
                return docRef.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Duyuru kaydedilemedi: " + ex.Message);
            }
        }
        public async Task<(List<string> fileNames, List<string> fileUrls, List<string> fileSizes)> UploadAnnouncementFilesAsync(List<string> filePaths)
        {
            try
            {
                List<string> fileNames = new List<string>();
                List<string> fileUrls = new List<string>();
                List<string> fileSizes = new List<string>();

                var storage = new FirebaseStorage("your-bucket-name.appspot.com");

                foreach (string filePath in filePaths)
                {
                    string fileName = Path.GetFileName(filePath);
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    using (var stream = new MemoryStream(fileBytes))
                    {
                        string downloadUrl = await storage
                            .Child("announcements")
                            .Child(Guid.NewGuid().ToString() + "_" + fileName)
                            .PutAsync(stream);

                        fileNames.Add(fileName);
                        fileUrls.Add(downloadUrl);

                        double mb = new FileInfo(filePath).Length / 1024d / 1024d;
                        fileSizes.Add(mb.ToString("0.0") + " MB");
                    }
                }

                return (fileNames, fileUrls, fileSizes);
            }
            catch (Exception ex)
            {
                throw new Exception("Dosyalar yüklenemedi: " + ex.Message);
            }
        }
    }


}
