using System.Collections.Generic;
using System.Threading.Tasks;

namespace Helo.ApiClient.Activity
{
    public interface IHeloActivityClient
    {
        Task<PaginatedEventsResponse> ListEvents(
            string channelId = null,
            string messageId = null,
            long? after = null,
            string startDate = null,
            string endDate = null,
            int? limit = null,
            string recipient = null,
            string subject = null,
            IEnumerable<string> tags = null,
            MailType? mailType = null,
            IEnumerable<EventType> eventTypes = null);

        Task<PaginatedMessagesResponse> ListMessages(
            string channelId = null,
            long? after = null,
            string startDate = null,
            string endDate = null,
            int? limit = null,
            string recipient = null,
            string subject = null,
            IEnumerable<string> tags = null,
            MailType? mailType = null,
            MessageStatus? status = null);

        Task<MessageDetailsResponse> RetrieveMessage(string id);
    }
}
