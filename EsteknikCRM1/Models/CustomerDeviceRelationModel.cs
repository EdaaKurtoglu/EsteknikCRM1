using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class CustomerDeviceRelationModel
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string DeviceId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
