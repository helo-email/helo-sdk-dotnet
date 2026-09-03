using System;

namespace HeloEmail.Sdk.Broadcasts
{
    public class BroadcastResponse
    {
        /// <summary>
        /// Unique broadcast identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// When the broadcast was submitted.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
        public BroadcastStatus Status { get; set; }

        /// <summary>
        /// Email subject line.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Processing progress as a percentage (e.g. `"45%"`).
        /// </summary>
        public string Completion { get; set; }

        /// <summary>
        /// Total number of messages in the broadcast.
        /// </summary>
        public int Messages { get; set; }
    }
}
