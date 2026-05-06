namespace EsteknikCRM.Entities
{
    public class WorkflowTeamOperation
    {
        public string Id { get; set; }
        public string WorkflowId { get; set; }
        public string CustomerId { get; set; }
        public string DeviceId { get; set; }
        public string SerialNumber { get; set; }
        public string StockCode { get; set; }
        public string DeviceName { get; set; }
        public string OperationType { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string CreatedByUserMail { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBySurname { get; set; }
        public string CreatedByRole { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CustomerOrCenterPay { get; set; }
        public string? LaborCode { get; set; }
        public string? LaborName { get; set; }
        public string? HakedisSetId { get; set; }
        public bool IsBilled { get; set; } = false;
        public int ApprovalStatus { get; set; } // 0,1,2
    }
}