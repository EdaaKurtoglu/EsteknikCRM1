using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class TeamItem
    {
        public string Id { get; set; }
        public string TeamId { get; set; }
        public string ServiceName { get; set; }
        public string TeamName { get; set; }
        public int MemberCount { get; set; }
        public string MemberName { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string VehiclePlate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PassiveDate { get; set; }
    }
}
