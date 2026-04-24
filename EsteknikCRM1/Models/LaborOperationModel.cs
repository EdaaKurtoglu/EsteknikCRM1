using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class LaborOperationModel
    {
        public string Id { get; set; } = "";
        public string LaborCode { get; set; } = "";
        public string LaborName { get; set; } = "";
        public string SubLaborName { get; set; } = "";
        public string Status { get; set; } = "";

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SubLaborName))
                    return LaborCode + " - " + LaborName;

                return LaborCode + " - " + LaborName + " - " + SubLaborName;
            }
        }
    }
}