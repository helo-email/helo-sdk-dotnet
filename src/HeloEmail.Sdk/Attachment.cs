namespace HeloEmail.Sdk
{
    public class Attachment
    {
        public string Content { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public AttachmentDisposition Disposition { get; set; }
    }

    public enum AttachmentDisposition
    {
        Attachment,
        Inline,
    }
}
