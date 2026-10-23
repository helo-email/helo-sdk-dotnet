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

        public Task<TemplateResponse> Create(CreateTemplateRequest request) =>
            Post<CreateTemplateRequest, TemplateResponse>("/templates", request);

        public Task<RenderTemplateResponse> RenderDraft(RenderDraftRequest request) =>
            Post<RenderDraftRequest, RenderTemplateResponse>("/templates/render", request);

        public Task<TemplateResponse> Retrieve(string id) =>
            Get<TemplateResponse>($"/templates/{Uri.EscapeDataString(id)}");

        public Task<TemplateResponse> Update(string id, UpdateTemplateRequest request) =>
            Patch<UpdateTemplateRequest, TemplateResponse>($"/templates/{Uri.EscapeDataString(id)}", request);

        public new Task Delete(string id) =>
            base.Delete($"/templates/{Uri.EscapeDataString(id)}");

        public Task<RenderTemplateResponse> Render(string id, RenderTemplateRequest request) =>
            Post<RenderTemplateRequest, RenderTemplateResponse>($"/templates/{Uri.EscapeDataString(id)}/render", request);
    }
}
