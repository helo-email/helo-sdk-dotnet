namespace HeloEmail.Sdk
{
    /// <summary>
    /// How the attachment is presented. Use `attachment` for downloadable files and `inline` for embedded content (e.g. images referenced in HTML).
    /// </summary>
    public enum AttachmentDisposition
    {
        Attachment,
        Inline,
    }
}
