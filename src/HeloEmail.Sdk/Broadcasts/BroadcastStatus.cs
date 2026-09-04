namespace HeloEmail.Sdk.Broadcasts
{
    /// <summary>
    /// Current processing state of a broadcast. `accepted` — queued, waiting for the channel to become available. `processing` — actively sending messages. `completed` — all messages have been processed. `canceled` — stopped before completing.
    /// </summary>
    public enum BroadcastStatus
    {
        Accepted,
        Processing,
        Completed,
        Canceled,
    }
}
