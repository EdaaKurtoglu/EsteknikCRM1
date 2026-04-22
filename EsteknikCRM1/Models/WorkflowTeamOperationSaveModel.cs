using System;

namespace EsteknikCRM1.Models
{
    public class WorkflowTeamOperationSaveModel
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
        public string CustomerOrCenterPay { get; set; } // 🔥 YENİ ALAN

        public string CreatedByUserMail { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBySurname { get; set; }
        public string CreatedByRole { get; set; }

        public DateTime CreatedDate { get; set; }
        public Boolean IsBilled { get; set; }
    }
}