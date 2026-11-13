using System.Collections.Generic;

namespace HeloEmail.Sdk.Templates
{
    /// <summary>
    /// At least one of html, mjml or text must be supplied. Supplying both html and mjml in  the same request is rejected - mjml content will be rendered to html. Providing raw html will override any mjml content stored on the template.
    /// </summary>
    public class CreateTemplateRequest
    {
        /// <summary>
        /// Maximum 64 characters.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Maximum 256 characters.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Maximum 500,000 characters.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Maximum 500,000 characters. The html it compiles to must also be within the html limit.
        /// </summary>
        public string Mjml { get; set; }

        /// <summary>
        /// Maximum 100,000 characters.
        /// </summary>
        public string Text { get; set; }
        public List<string> ChannelIds { get; set; }
    }
}
