namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// The mail client the recipient engaged with the message from.
    /// </summary>
    public class ClientDetails
    {
        public string Family { get; set; }
        public string Version { get; set; }
    }
}
