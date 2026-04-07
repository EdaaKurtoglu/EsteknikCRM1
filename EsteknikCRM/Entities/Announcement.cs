namespace EsteknikCRM.Entities
{
    public class Announcement
    {
        public string Subject { get; set; }
        public string Id { get; set; }
        public string DateText { get; set; }
        public string BodyText { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentSizeText { get; set; }
        public string GroupText { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<string> FileNames { get; set; } = new List<string>();
        public List<string> FileUrls { get; set; } = new List<string>();
        public List<string> FileSizes { get; set; } = new List<string>();
    }
}
