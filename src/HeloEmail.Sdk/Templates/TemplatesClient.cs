using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk.Templates
{
    public class TemplatesClient : BaseClient, ITemplatesClient
    {
        public TemplatesClient([FromKeyedServices(KeyedServices.HeloApiClientName)] HttpClient httpClient,
            ILogger<TemplatesClient> logger) :
            base(httpClient, logger)
        {
        }

        /// <summary>
        /// List all templates
        /// </summary>
        public Task<PaginatedResponseOfTemplateSummaryResponse> List(
            int? limit = null,
            int? offset = null,
            string name = null,
            IEnumerable<string> channelIds = null)
        {
            var query = new List<(string, string)>
            {
                ("limit", limit?.ToString(CultureInfo.InvariantCulture)),
                ("offset", offset?.ToString(CultureInfo.InvariantCulture)),
                ("name", name),
            };

            if (channelIds != null)
                query.AddRange(channelIds.Select(x => ("channelIds", x)));

            return Get<PaginatedResponseOfTemplateSummaryResponse>(BuildUrl("/templates", query));
        }

        /// <summary>
        /// Create a template
        /// </summary>
        public Task<TemplateResponse> Create(CreateTemplateRequest request) =>
            Post<CreateTemplateRequest, TemplateResponse>("/templates", request);

        /// <summary>
        /// Render template content
        /// </summary>
        public Task<RenderTemplateResponse> RenderDraft(RenderDraftRequest request) =>
            Post<RenderDraftRequest, RenderTemplateResponse>("/templates/render", request);

        /// <summary>
        /// Retrieve a template
        /// </summary>
        public Task<TemplateResponse> Retrieve(string id) =>
            Get<TemplateResponse>($"/templates/{Uri.EscapeDataString(id)}");

        /// <summary>
        /// Update a template
        /// </summary>
        public Task<TemplateResponse> Update(string id, UpdateTemplateRequest request) =>
            Patch<UpdateTemplateRequest, TemplateResponse>($"/templates/{Uri.EscapeDataString(id)}", request);

        /// <summary>
        /// Delete a template
        /// </summary>
        public new Task Delete(string id) =>
            base.Delete($"/templates/{Uri.EscapeDataString(id)}");

        /// <summary>
        /// Render a template
        /// </summary>
        public Task<RenderTemplateResponse> Render(string id, RenderTemplateRequest request) =>
            Post<RenderTemplateRequest, RenderTemplateResponse>($"/templates/{Uri.EscapeDataString(id)}/render", request);
    }
}
