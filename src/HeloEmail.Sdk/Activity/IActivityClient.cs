using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Activity
{
    public interface IActivityClient
    {
        /// <summary>
        /// List activity events
        /// </summary>
        Task<PaginatedEventsResponse> ListEvents(
            string channelId = null,
            string messageId = null,
            long? after = null,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null,
            int? limit = null,
            string recipient = null,
            string subject = null,
            IEnumerable<string> tags = null,
            MailType? mailType = null,
            IEnumerable<EventType> eventTypes = null);

        /// <summary>
        /// List messages
        /// </summary>
        Task<PaginatedMessagesResponse> ListMessages(
            string channelId = null,
            long? after = null,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null,
            int? limit = null,
            string recipient = null,
            string subject = null,
            IEnumerable<string> tags = null,
            MailType? mailType = null,
            Status? status = null);

        /// <summary>
        /// Retrieve message details
        /// </summary>
        Task<MessageDetailsResponse> RetrieveMessage(string id);
    }
}
