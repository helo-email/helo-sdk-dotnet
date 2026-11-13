namespace HeloEmail.Sdk.Templates
{
    /// <summary>
    /// Supplying both html and mjml in the same request is rejected.
    /// </summary>
    public class RenderDraftRequest
    {
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

        /// <summary>
        /// Inline CSS into the HTML, as a send does. Defaults to true, so the preview matches what recipients receive.
        /// </summary>
        public bool? InlineStyles { get; set; }

        /// <summary>
        /// Sample data used to interpolate Liquid variables.
        /// </summary>
        public object Data { get; set; }
    }
}
