using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EsteknikCRM1.Models
{
    public class ProductLaborPriceModel
    {
        public string Id { get; set; }
        public string ProductCode { get; set; }
        public string StockCode { get; set; }
        public string ProductName { get; set; }

        public string LaborCode { get; set; }
        public string LaborName { get; set; }
        public string SubLaborName { get; set; }

        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}