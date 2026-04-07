using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class OperationService
    {
        private readonly FirestoreDb db;

        public OperationService(FirestoreDb _db)
        {
            db = _db;
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


    }
}
