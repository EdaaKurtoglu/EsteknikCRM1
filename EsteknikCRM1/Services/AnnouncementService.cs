using EsteknikCRM1.Models;
using Firebase.Storage;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class AnnouncementService
    {
        private readonly FirestoreDb db;

        public AnnouncementService(FirestoreDb _db)
        {
            db = _db;
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
