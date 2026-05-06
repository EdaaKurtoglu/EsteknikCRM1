using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class CustomerService
    {
        private readonly FirestoreDb db;

        public CustomerService(FirestoreDb _db) {
            db = _db;
        }
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

                    Status = data.ContainsKey("Status") ? data["Status"]?.ToString() : "active"
                });
            }

            return customers;
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

                { "Status", customer.Status ?? "active" }
            };

            DocumentReference addedDoc = await customersRef.AddAsync(data);
            return addedDoc.Id;
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

    }
}
