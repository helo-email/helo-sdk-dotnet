namespace HeloEmail.Sdk
{
    public class MailAddress
    {
        /// <summary>
        /// Email address. Max 254 characters.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Display name shown to recipients in their email client.
        /// </summary>
        public string Name { get; set; }
    }
}
