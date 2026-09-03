using System;

namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastDetailsResponse
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public BroadcastStatus Status { get; set; }
        public string Subject { get; set; }
        public string Completion { get; set; }
        public int Messages { get; set; }

        /// <summary>
        /// Number of messages that failed permanently. Retrieve details via `GET /broadcasts/{id}/failures`.
        /// </summary>
        public int Failed { get; set; }

        /// <summary>
        /// Number of recipients skipped because they appear on a suppression list. Retrieve details via `GET /broadcasts/{id}/suppressions`.
        /// </summary>
        public int Suppressed { get; set; }
        public BroadcastContent Content { get; set; }
        public BroadcastTracking Tracking { get; set; }
        public BroadcastStatistics Statistics { get; set; }
    }
}
