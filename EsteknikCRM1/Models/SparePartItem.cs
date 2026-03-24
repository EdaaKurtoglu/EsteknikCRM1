using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class SparePartItem
    {
        public int Id { get; set; }
        public string SparePartCode { get; set; }
        public string SparePartName { get; set; }
        public string Vat { get; set; }
        public string Price { get; set; }
        public string WarrantyPeriod { get; set; }
        public string Supplier { get; set; }
        public string BCode { get; set; }
        public string Status { get; set; }
    }
}
