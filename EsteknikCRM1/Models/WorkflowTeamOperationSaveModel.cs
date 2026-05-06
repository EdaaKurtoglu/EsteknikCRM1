using System;
using System.ComponentModel;

namespace EsteknikCRM1.Models
{
    public class WorkflowTeamOperationSaveModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int _approvalStatus;
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
        public int ApprovalStatus
        {
            get => _approvalStatus;
            set
            {
                _approvalStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ApprovalStatus)));
            }
        }
        public string CreatedByUserMail { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBySurname { get; set; }
        public string CreatedByRole { get; set; }
        public string LaborCode { get; set; }
        public string LaborName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsBilled { get; set; }
    }
}