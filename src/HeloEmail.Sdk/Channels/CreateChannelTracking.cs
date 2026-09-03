namespace HeloEmail.Sdk.Channels
{
    public class CreateChannelTracking
    {
        /// <summary>
        /// Whether to track link clicks in outgoing emails.
        /// </summary>
        public bool? Links { get; set; }

        /// <summary>
        /// Whether to track email opens.
        /// </summary>
        public bool? Opens { get; set; }
    }
}
