namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastStatistics
    {
        /// <summary>
        /// Total messages submitted to the mail transfer agent.
        /// </summary>
        public int Sent { get; set; }

        /// <summary>
        /// Messages accepted by the recipient mail server.
        /// </summary>
        public int Delivered { get; set; }

        /// <summary>
        /// Messages rejected by the recipient mail server.
        /// </summary>
        public int Bounced { get; set; }

        /// <summary>
        /// Unique recipients who opened the email (requires open tracking).
        /// </summary>
        public int Opened { get; set; }

        /// <summary>
        /// Unique recipients who clicked a tracked link (requires link tracking).
        /// </summary>
        public int Clicked { get; set; }

        /// <summary>
        /// Recipients who marked the email as spam.
        /// </summary>
        public int Complained { get; set; }

        /// <summary>
        /// Recipients who clicked the unsubscribe link.
        /// </summary>
        public int Unsubscribed { get; set; }
    }
}
