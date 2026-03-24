using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string TopGroup { get; set; }
        public string SubGroup { get; set; }
        public string SpecialGroup { get; set; }
        public string CostCenter { get; set; }
        public string WarrantyMonth { get; set; }
        public string Price { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
    }
}
