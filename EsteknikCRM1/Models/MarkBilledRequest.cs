using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class MarkBilledRequest
    {
        public string HakedisSetId { get; set; }
        public List<string> OperationIds { get; set; }
    }
}
