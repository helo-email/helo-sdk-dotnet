using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Activity
{
    public class ActivityClient : BaseClient, IActivityClient
    {
        public ActivityClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<ActivityClient> logger) :
            base(httpClient, logger)
        {
        }

        /// <summary>
        /// List activity events
        /// </summary>
        public Task<PaginatedEventsResponse> ListEvents(
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
            IEnumerable<EventType> eventTypes = null)
        {
            var query = new List<(string, string)>
            {
                ("channelId", channelId),
                ("messageId", messageId),
                ("after", after?.ToString(CultureInfo.InvariantCulture)),
                ("startDate", startDate?.ToString("O")),
                ("endDate", endDate?.ToString("O")),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("recipient", recipient),
                ("subject", subject),
                ("mailType", ToQueryValue(mailType)),
            };

            if (tags != null)
                query.AddRange(tags.Select(x => ("tags", x)));

            if (eventTypes != null)
                query.AddRange(eventTypes.Select(x => ("eventTypes", ToQueryValue(x))));

            return Get<PaginatedEventsResponse>(BuildUrl("/activity/events", query));
        }

        /// <summary>
        /// List messages
        /// </summary>
        public Task<PaginatedMessagesResponse> ListMessages(
            string channelId = null,
            long? after = null,
            DateTimeOffset? startDate = null,
            DateTimeOffset? endDate = null,
            int? limit = null,
            string recipient = null,
            string subject = null,
            IEnumerable<string> tags = null,
            MailType? mailType = null,
            MessageStatus? status = null)
        {
            var query = new List<(string, string)>
            {
                ("channelId", channelId),
                ("after", after?.ToString(CultureInfo.InvariantCulture)),
                ("startDate", startDate?.ToString("O")),
                ("endDate", endDate?.ToString("O")),
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("recipient", recipient),
                ("subject", subject),
                ("mailType", ToQueryValue(mailType)),
                ("status", ToQueryValue(status)),
            };

            if (tags != null)
                query.AddRange(tags.Select(x => ("tags", x)));

            return Get<PaginatedMessagesResponse>(BuildUrl("/activity/messages", query));
        }

        /// <summary>
        /// Retrieve message details
        /// </summary>
        public Task<MessageDetailsResponse> RetrieveMessage(string id) =>
            Get<MessageDetailsResponse>($"/activity/messages/{Uri.EscapeDataString(id)}");
    }
}
