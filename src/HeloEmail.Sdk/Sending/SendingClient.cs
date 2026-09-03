using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Sending
{
    public class SendingClient : BaseClient, ISendingClient
    {
        public SendingClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<SendingClient> logger) :
            base(httpClient, logger)
        {
        }

        /// <summary>
        /// Send a transactional email
        /// </summary>
        public Task<SendMessageAcceptedResponse> Transactional(
            SendMessageRequest request,
            string channelId = null,
            string idempotencyKey = null)
        {
            var headers = new Dictionary<string, string>();
            if (channelId != null)
                headers["X-Helo-Channel-Id"] = channelId;
            if (idempotencyKey != null)
                headers["X-Helo-Idempotency-Key"] = idempotencyKey;

            return Post<SendMessageRequest, SendMessageAcceptedResponse>("/send/transactional", request, headers);
        }

        /// <summary>
        /// Send transactional emails in batch
        /// </summary>
        public Task<SendMessageBatchResponse> TransactionalBatch(
            SendMessageBatchRequest request,
            string channelId = null,
            string idempotencyKey = null)
        {
            var headers = new Dictionary<string, string>();
            if (channelId != null)
                headers["X-Helo-Channel-Id"] = channelId;
            if (idempotencyKey != null)
                headers["X-Helo-Idempotency-Key"] = idempotencyKey;

            return Post<SendMessageBatchRequest, SendMessageBatchResponse>("/send/transactional/batch", request, headers);
        }

        /// <summary>
        /// Send a broadcast email
        /// </summary>
        public Task<SendBroadcastResponse> Broadcast(
            SendBroadcastRequest request,
            string channelId = null,
            string idempotencyKey = null)
        {
            var headers = new Dictionary<string, string>();
            if (channelId != null)
                headers["X-Helo-Channel-Id"] = channelId;
            if (idempotencyKey != null)
                headers["X-Helo-Idempotency-Key"] = idempotencyKey;

            return Post<SendBroadcastRequest, SendBroadcastResponse>("/send/broadcast", request, headers);
        }

        /// <summary>
        /// Send a single broadcast email
        /// </summary>
        public Task<SendMessageAcceptedResponse> BroadcastMessage(
            SendMessageRequest request,
            string channelId = null,
            string idempotencyKey = null)
        {
            var headers = new Dictionary<string, string>();
            if (channelId != null)
                headers["X-Helo-Channel-Id"] = channelId;
            if (idempotencyKey != null)
                headers["X-Helo-Idempotency-Key"] = idempotencyKey;

            return Post<SendMessageRequest, SendMessageAcceptedResponse>("/send/broadcast/message", request, headers);
        }
    }
}
