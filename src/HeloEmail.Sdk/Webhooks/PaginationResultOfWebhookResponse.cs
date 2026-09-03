using System.Collections.Generic;

namespace HeloEmail.Sdk.Webhooks
{
    /// <summary>
    /// Paginated list of webhooks.
    /// </summary>
    public class PaginationResultOfWebhookResponse
    {
        /// <summary>
        /// The page of results.
        /// </summary>
        public List<WebhookResponse> Results { get; set; }

        /// <summary>
        /// Total number of webhooks matching the query.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
