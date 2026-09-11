using System.Threading.Tasks;

namespace HeloEmail.Sdk.Sending
{
    public interface ISendingClient
    {
        /// <summary>
        /// Send a transactional email
        /// </summary>
        Task<SendMessageAcceptedResponse> SendTransactional(
            SendMessageRequest request,
            string channelId = null,
            string idempotencyKey = null);

        /// <summary>
        /// Send transactional emails in batch
        /// </summary>
        Task<SendMessageBatchResponse> SendTransactionalBatch(
            SendMessageBatchRequest request,
            string channelId = null,
            string idempotencyKey = null);

        Task<SendBroadcastResponse> SendBroadcast(
            SendBroadcastRequest request,
            string channelId = null,
            string idempotencyKey = null);

        /// <summary>
        /// Send a single broadcast email
        /// </summary>
        Task<SendMessageAcceptedResponse> SendBroadcastMessage(
            SendMessageRequest request,
            string channelId = null,
            string idempotencyKey = null);
    }
}
