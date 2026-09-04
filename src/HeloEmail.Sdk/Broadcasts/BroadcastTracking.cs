namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastTracking
    {
        /// <summary>
        /// Whether open tracking is enabled for this broadcast.
        /// </summary>
        public bool Opens { get; set; }

        /// <summary>
        /// Whether link click tracking is enabled for this broadcast.
        /// </summary>
        public bool Links { get; set; }
    }
}
