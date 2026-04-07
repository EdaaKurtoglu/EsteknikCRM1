using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class WorkflowService
    {
        private readonly FirestoreDb db;

        public WorkflowService(FirestoreDb _db) { 
            db = _db;
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

    }
}
