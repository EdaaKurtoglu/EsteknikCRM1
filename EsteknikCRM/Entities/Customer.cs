namespace EsteknikCRM.Entities
{
    public class Customer
    {
        public string Id { get; set; }

        public bool IsVip { get; set; }

        public string CustomerNo { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }

        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Phone2 { get; set; }
        public string MobilePhone2 { get; set; }

        public bool UnknownEmail { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string Address { get; set; }

        public string SpecialProjectInfo { get; set; }
        public string BuildingInfo { get; set; }

        public string Status { get; set; }
        public string FullName
        {
            get
            {
                return ($"{Name} {MiddleName} {Surname}").Replace("  ", " ").Trim();
            }
        }
    }
}
