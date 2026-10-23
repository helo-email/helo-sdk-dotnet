namespace HeloEmail.Sdk.Templates
{
    /// <summary>
    /// Supplying both html and mjml in the same request is rejected.
    /// </summary>
    public class RenderDraftRequest
    {
        public string Subject { get; set; }
        public string Html { get; set; }
        public string Mjml { get; set; }
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
