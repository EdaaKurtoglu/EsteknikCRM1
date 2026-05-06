using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class DeviceService
    {
        private readonly FirestoreDb db;
        public DeviceService(FirestoreDb _db) {
            db = _db;
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
            { "Status", device.Status ?? "active" }
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
                        CommissionDate = doc.ContainsField("CommissionDate") ? doc.GetValue<Timestamp>("CommissionDate").ToDateTime() : (DateTime?)null,
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

    }
}
