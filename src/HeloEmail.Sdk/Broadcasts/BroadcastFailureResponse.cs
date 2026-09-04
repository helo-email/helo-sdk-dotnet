namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastFailureResponse
    {
        public RecipientHeaders Recipients { get; set; }

        /// <summary>
        /// Zero-based index of the failed message within the original `messages` array.
        /// </summary>
        public int MessageIndex { get; set; }

        /// <summary>
        /// Machine-readable error code (e.g. `invalid_recipient`, `domain_unverified`).
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Human-readable description of the failure.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
