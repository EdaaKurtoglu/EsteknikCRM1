using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class AddressService
    {
        private readonly FirestoreDb db;

        public AddressService(FirestoreDb _db)
        {
            db = _db;
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
                    { "Status", address.Status ?? "acttive" },
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
                        Status = data.ContainsKey("Status") ? data["Status"]?.ToString() : "active",
                        IsActive = data.ContainsKey("IsActive") && data["IsActive"] is bool active && active,
                        IsResidence = data.ContainsKey("IsResidence") && data["IsResidence"] is bool residence && residence
                    });
                }
            }

            return addresses;
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

    }
}
