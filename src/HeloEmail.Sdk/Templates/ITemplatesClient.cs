using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeloEmail.Sdk.Templates
{
    public interface ITemplatesClient
    {
        /// <summary>
        /// List all templates
        /// </summary>
        Task<PaginatedResponseOfTemplateSummaryResponse> List(
            int? limit = null,
            int? offset = null,
            string name = null,
            IEnumerable<string> channelIds = null);

        /// <summary>
        /// Create a template
        /// </summary>
        Task<TemplateResponse> Create(CreateTemplateRequest request);

        /// <summary>
        /// Render template content
        /// </summary>
        Task<RenderTemplateResponse> RenderDraft(RenderDraftRequest request);

        /// <summary>
        /// Retrieve a template
        /// </summary>
        Task<TemplateResponse> Retrieve(string id);

        /// <summary>
        /// Update a template
        /// </summary>
        Task<TemplateResponse> Update(string id, UpdateTemplateRequest request);

        /// <summary>
        /// Delete a template
        /// </summary>
        Task Delete(string id);

        /// <summary>
        /// Render a template
        /// </summary>
        Task<RenderTemplateResponse> Render(string id, RenderTemplateRequest request);
    }
}
