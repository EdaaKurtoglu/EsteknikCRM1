using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class AddressModel
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }

        public string AddressLine { get; set; }
        public string FlatNo { get; set; }
        public string BuildingNo { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public bool IsResidence { get; set; }
        public string Status { get; set; }
        public string OwnershipType { get; set; }
        public string PostCode { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PassiveDate { get; set; }
        public string IsResidenceText
        {
            get { return IsResidence ? "Evet" : "Hayır"; }
        }
    }
}
