namespace EsteknikCRM.Api.Entities
{
    public class AnnouncementFile
    {
        public string Id { get; set; }
        public string AnnouncementId { get; set; }
        public string FileName { get; set; }
        public string StoredFileName { get; set; }
        public string FilePath { get; set; }
        public string? ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}