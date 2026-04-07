using Google.Cloud.Firestore;
using System;
using System.IO;

namespace EsteknikCRM1.Services.Infrastructure
{
    public class FirebaseContext
    {
        public FirestoreDb Db { get; }

        public FirebaseContext()
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "serviceAccountKey.json");

            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS", path);

            Db = FirestoreDb.Create("esteknikcrm");
        }
    }
}