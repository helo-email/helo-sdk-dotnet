namespace HeloEmail.Sdk.Sending
{
    public class SendBroadcastResponse
    {
        /// <summary>
        /// `accepted` — broadcast queued successfully. `failed` — validation or account error (see `errorCode`). `delayed` — transient submission error, will be retried automatically.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Unique identifier for the broadcast. Use this to track progress via `GET /broadcasts/{id}`.
        /// </summary>
        public string BroadcastId { get; set; }
    }
}
