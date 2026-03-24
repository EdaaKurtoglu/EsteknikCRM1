using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class ReturnSetItem
    {
        public int Id { get; set; }
        public string ReturnSetType { get; set; }
        public string ProcessType { get; set; }
        public string ServiceName { get; set; }
        public string FlowNumber { get; set; }
        public string Description { get; set; }
        public string Completed { get; set; }
        public string SetDate { get; set; }
        public string ItemCount { get; set; }
    }
}
