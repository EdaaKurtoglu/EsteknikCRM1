using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class HakedisOperationItem
    {
        public int Id { get; set; }
        public string WorkflowReceiptNo { get; set; }
        public string Customer { get; set; }
        public string ServiceReceiptType { get; set; }
        public string DeviceSerialNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string LaborName { get; set; }
        public string SubLaborName { get; set; }
        public string Amount { get; set; }
        public string Quantity { get; set; }
    }
}
