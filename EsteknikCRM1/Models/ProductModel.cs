using System;

namespace EsteknikCRM1.Models
{
    public class ProductModel
    {
        public string Id { get; set; }

        public string ProductCode { get; set; }
        public string ProductNameTr { get; set; }
        public string ProductNameEn { get; set; }

        public string CostCenter { get; set; }
        public string BCode { get; set; }
        public string ProductManager { get; set; }

        public int WarrantyMonth1 { get; set; }
        public int WarrantyMonth2 { get; set; }

        public bool Ewl { get; set; }
        public DateTime? EwlStartDate { get; set; }
        public DateTime? EwlEndDate { get; set; }

        public string Brand { get; set; }
        public string TopGroup { get; set; }
        public string SubGroup { get; set; }
        public string SpecialGroup { get; set; }

        public string Country { get; set; }

        public bool CascadeSystem { get; set; }
        public bool PhaseOut { get; set; }
        public DateTime? PhaseOutDate { get; set; }

        public bool SapPhaseOut { get; set; }
        public DateTime? SapPhaseOutDate { get; set; }

        public bool NoSerialNumber { get; set; }
        public string ProductionPlace { get; set; }
        public string SalesInfo { get; set; }

        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}