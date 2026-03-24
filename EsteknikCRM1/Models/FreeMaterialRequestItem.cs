using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class FreeMaterialRequestItem
    {
        public int Id { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string ServiceReceipt { get; set; }
        public string ServiceSapCode { get; set; }
        public string Customer { get; set; }
        public string SparePartProductName { get; set; }
        public string SparePartProductSapCode { get; set; }
        public string TtsmOrderNumber { get; set; }
        public string SapOrderNumber { get; set; }
        public string CreatedDate { get; set; }
        public string WorkflowNo { get; set; }
        public string OrderType { get; set; }
        public string ReturnSetNo { get; set; }
    }
}
