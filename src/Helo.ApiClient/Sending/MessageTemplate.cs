namespace Helo.ApiClient.Sending
{
    public class MessageTemplate
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
        public bool? InlineStyles { get; set; }
        public object Data { get; set; }
    }
}
