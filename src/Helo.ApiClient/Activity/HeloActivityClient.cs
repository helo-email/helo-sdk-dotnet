using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.Activity
{
    public class HeloActivityClient : HeloBaseClient, IHeloActivityClient
    {
        public HeloActivityClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<HeloActivityClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<PaginatedEventsResponse> ListEvents(
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
            IEnumerable<EventType> eventTypes = null)
        {
            var query = new List<(string, string)>
            {
                ("channelId", channelId),
                ("messageId", messageId),
                ("after", after?.ToString()),
                ("startDate", startDate),
                ("endDate", endDate),
                ("limit", limit?.ToString()),
                ("recipient", recipient),
                ("subject", subject),
                ("mailType", mailType?.ToString().ToLower()),
            };

            if (tags != null)
            {
                query.AddRange(tags.Select(tag => ("tags", tag)));
            }

            if (eventTypes == null)
            {
                return Get<PaginatedEventsResponse>(BuildUrl("/activity/events", query));
            }

            query.AddRange(eventTypes.Select(et => ("eventTypes", et.ToString().ToLower())));

            return Get<PaginatedEventsResponse>(BuildUrl("/activity/events", query));
        }

        public Task<PaginatedMessagesResponse> ListMessages(
            string channelId = null,
            long? after = null,
            string startDate = null,
            string endDate = null,
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
                ("after", after?.ToString()),
                ("startDate", startDate),
                ("endDate", endDate),
                ("limit", limit?.ToString()),
                ("recipient", recipient),
                ("subject", subject),
                ("mailType", mailType?.ToString().ToLower()),
                ("status", status?.ToString().ToLower()),
            };

            if (tags == null)
            {
                return Get<PaginatedMessagesResponse>(BuildUrl("/activity/messages", query));
            }

            query.AddRange(tags.Select(tag => ("tags", tag)));

            return Get<PaginatedMessagesResponse>(BuildUrl("/activity/messages", query));
        }

        public Task<MessageDetailsResponse> RetrieveMessage(string id) =>
            Get<MessageDetailsResponse>($"/activity/messages/{Uri.EscapeDataString(id)}");

    }
}