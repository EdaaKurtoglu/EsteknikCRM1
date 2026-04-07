using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class CustomerDeviceService
    {
        private readonly FirestoreDb db;

        public CustomerDeviceService(FirestoreDb _db) {
            db = _db;
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
                var relations = await GetCustomerDeviceRelationsAsync(customerId);
                var devices = new List<DeviceModel>();

                foreach (var relation in relations)
                {
                    if (string.IsNullOrWhiteSpace(relation.DeviceId))
                        continue;

                    var device = await AppServices.DeviceService.GetDeviceByIdAsync(relation.DeviceId);
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

    }
}
