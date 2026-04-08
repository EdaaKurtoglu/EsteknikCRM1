namespace EsteknikCRM.Api.Entities
{
    public class CustomerDevice
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string DeviceId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
