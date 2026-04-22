namespace EsteknikCRM.Entities
{
    public class HakedisSet
    {
        public string Id { get; set; }

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

        public string PayType { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}