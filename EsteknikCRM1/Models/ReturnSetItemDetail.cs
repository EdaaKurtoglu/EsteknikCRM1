using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class ReturnSetItemDetail
    {
        public int Id { get; set; }
        public string ReturnProcessSetId { get; set; }
        public string ServiceName { get; set; }
        public string SparePartName { get; set; }
        public string SparePartSapCode { get; set; }
        public string TtsmOrderNumber { get; set; }
        public string SapOrderNumber { get; set; }
        public string DeliveryNumber { get; set; }
        public string ReceiptNumber { get; set; }
        public string ServiceReceiptNumber { get; set; }
        public string ReceiptArrivalDate { get; set; }
    }
}
