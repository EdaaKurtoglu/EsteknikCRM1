using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    namespace EsteknikCRM1.Models
    {
        public class WorkflowTeamOperationItem
        {
            public string DeviceId { get; set; }
            public string SerialNumber { get; set; }
            public string StockCode { get; set; }
            public string DeviceName { get; set; }
            public string OperationType { get; set; }

            public decimal Price { get; set; }
            public int Quantity { get; set; } = 1;
            public decimal TotalAmount
            {
                get { return Price * Quantity; }
            }
        }
    }
}
