namespace EsteknikCRM.Entities
{
    public class Workflow
    {
        public string Id { get; set; }
        public string WorkflowId { get; set; }
        public string StartType { get; set; }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerPhone { get; set; }
        public string AddressId { get; set; }
        public string AddressLine { get; set; }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string NotificationTypeId { get; set; }
        public string NotificationTypeName { get; set; }

        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public string DeviceId { get; set; }
        public string DeviceName { get; set; }

        public string Description { get; set; }
        public string ExtraDescription { get; set; }
        public string ArrivalChannel { get; set; }

        public string WorkflowStatus { get; set; }
        public string FlowType { get; set; }
        public string Subject { get; set; }

        public string CreatedByUserMail { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedBySurname { get; set; }
        public string CreatedByFullName { get; set; }
        public string CreatedByRole { get; set; }

        public string WorkTeam { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastAction { get; internal set; }
    }
}
