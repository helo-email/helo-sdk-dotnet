namespace HeloEmail.Sdk.Errors
{
    /// <summary>
    /// A single field validation error.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Description of the validation failure.
        /// </summary>
        public string Message { get; set; }
    }
}
