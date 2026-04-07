using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient.Sending
{
    public class HeloSendingClient : HeloBaseClient, IHeloSendingClient
    {
        public HeloSendingClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<HeloSendingClient> logger) :
            base(httpClient, logger)
        {
        }

        public Task<SendMessageAcceptedResponse> Transactional(SendMessageRequest request,
            string channelId = null, string idempotencyKey = null) =>
            Post<SendMessageRequest, SendMessageAcceptedResponse>(
                "/send/transactional", request, BuildHeaders(channelId, idempotencyKey));

        public Task<SendMessageBatchResponse> TransactionalBatch(SendMessageBatchRequest request,
            string channelId = null, string idempotencyKey = null) =>
            Post<SendMessageBatchRequest, SendMessageBatchResponse>(
                "/send/transactional/batch", request, BuildHeaders(channelId, idempotencyKey));

        public Task<SendBroadcastResponse> Broadcast(SendBroadcastRequest request,
            string channelId = null, string idempotencyKey = null) =>
            Post<SendBroadcastRequest, SendBroadcastResponse>(
                "/send/broadcast", request, BuildHeaders(channelId, idempotencyKey));

        public Task<SendMessageAcceptedResponse> BroadcastMessage(SendMessageRequest request,
            string channelId = null, string idempotencyKey = null) =>
            Post<SendMessageRequest, SendMessageAcceptedResponse>(
                "/send/broadcast/message", request, BuildHeaders(channelId, idempotencyKey));

        private static Dictionary<string, string> BuildHeaders(string channelId, string idempotencyKey)
        {
            if (channelId == null && idempotencyKey == null)
                return null;

            var headers = new Dictionary<string, string>();
            if (channelId != null)
                headers["X-Helo-Channel-Id"] = channelId;
            if (idempotencyKey != null)
                headers["X-Helo-Idempotency-Key"] = idempotencyKey;
            return headers;
        }
    }
}
