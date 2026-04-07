using EsteknikCRM1.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Services
{
    public class TeamService
    {
        private readonly FirestoreDb db;

        public TeamService(FirestoreDb _db) {
            db = _db;
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

    }
}
