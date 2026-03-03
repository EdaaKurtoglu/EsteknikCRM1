using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    internal class SparePartModel
    {
       
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Vat { get; set; }
        public decimal Price { get; set; }
        public int Warranty { get; set; }
        public string Supplier { get; set; }
        public string BCode { get; set; }
        public string Status { get; set; }

        public string PriceFormatted =>
            $"₺ {Price:N2} TRL";
    
    }
}
