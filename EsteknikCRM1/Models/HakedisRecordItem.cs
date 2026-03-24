using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class HakedisRecordItem
    {
        public int Id { get; set; }
        public string ServiceTitle { get; set; }
        public string SapServiceCode { get; set; }
        public string ServiceResponsible { get; set; }
        public int GreenCount { get; set; }
        public int BlueCount { get; set; }
        public int RedCount { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string PreApprovalDate { get; set; }
        public string PreApprovalApproveDate { get; set; }
        public string SetDate { get; set; }
        public string SetApproveDate { get; set; }
        public string ExportDate { get; set; }
    }
}
