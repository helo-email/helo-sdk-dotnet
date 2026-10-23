using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Templates
{
    public interface ITemplatesClient
    {
        Task<PaginatedResponseOfTemplateSummaryResponse> List(
            int? limit = null,
            int? offset = null,
            string name = null,
            IEnumerable<string> channelIds = null);

        Task<TemplateResponse> Create(CreateTemplateRequest request);

        Task<RenderTemplateResponse> RenderDraft(RenderDraftRequest request);

        Task<TemplateResponse> Retrieve(string id);

        Task<TemplateResponse> Update(string id, UpdateTemplateRequest request);

        Task Delete(string id);

        Task<RenderTemplateResponse> Render(string id, RenderTemplateRequest request);
    }
}
