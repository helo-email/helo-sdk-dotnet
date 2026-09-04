namespace HeloEmail.Sdk.Sending
{
    public class Attachment
    {
        /// <summary>
        /// Base64-encoded file content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Used to reference inline images in HTML (e.g. `cid:logo`). Only applicable when `disposition` is `inline`.
        /// </summary>
        public string ContentId { get; set; }

        /// <summary>
        /// MIME type (e.g. `application/pdf`). Inferred from the file extension if not provided.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// File name including extension (e.g. `report.pdf`). Executable and potentially harmful file types are blocked.
        /// </summary>
        public string FileName { get; set; }
        public AttachmentDisposition Disposition { get; set; }
    }
}
