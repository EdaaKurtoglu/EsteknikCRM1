namespace EsteknikCRM.Api.Entities
{
    public class LaborOperation
    {
        public string Id { get; set; }
        public string LaborCode { get; set; }
        public string LaborName { get; set; }
        public string? SubLaborName { get; set; }
        public string? Status { get; set; }
    }
}
