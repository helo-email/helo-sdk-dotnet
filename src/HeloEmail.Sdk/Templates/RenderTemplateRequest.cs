namespace HeloEmail.Sdk.Templates
{
    public class RenderTemplateRequest
    {
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
