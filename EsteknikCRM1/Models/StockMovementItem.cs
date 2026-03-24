using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class StockMovementItem
    {
        public int Id { get; set; }
        public string ExtraDescription2 { get; set; }
        public string ServiceName { get; set; }
        public string TeamName { get; set; }
        public string Date { get; set; }
        public string StockType { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public string MovementType { get; set; }
        public int Quantity { get; set; }
        public int Balance { get; set; }
        public string Description { get; set; }
    }
}
