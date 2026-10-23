using System.Collections.Generic;

namespace HeloEmail.Sdk.Templates
{
    /// <summary>
    /// At least one of html, mjml or text must be supplied. Supplying both html and mjml in  the same request is rejected - mjml content will be rendered to html. Providing raw html will override any mjml content stored on the template.
    /// </summary>
    public class CreateTemplateRequest
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Mjml { get; set; }
        public string Text { get; set; }
        public List<string> ChannelIds { get; set; }
    }
}
