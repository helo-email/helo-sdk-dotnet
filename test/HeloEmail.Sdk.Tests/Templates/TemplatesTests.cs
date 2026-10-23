using HeloEmail.Sdk.Templates;
using Microsoft.Extensions.Logging.Abstractions;

namespace HeloEmail.Sdk.Tests.Templates;

public class TemplatesTests : BaseFixture
{
    private static (TemplatesClient Client, StubHandler Handler) CreateClient()
    {
        var (httpClient, handler) = CreateHttpClient();
        return (new TemplatesClient(httpClient, NullLogger<TemplatesClient>.Instance), handler);
    }

    [Fact]
    public async Task List_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.List();

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/templates", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Create_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Create(new CreateTemplateRequest
        {
            Name = "test-name",
            Subject = "test-subject",
            Html = "test-html",
            Mjml = "test-mjml",
            Text = "test-text",
            ChannelIds = ["550e8400-e29b-41d4-a716-446655440000"],
        });

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/templates", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task RenderDraft_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.RenderDraft(new RenderDraftRequest
        {
            Subject = "test-subject",
            Html = "test-html",
            Mjml = "test-mjml",
            Text = "test-text",
            InlineStyles = true,
        });

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/templates/render", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Retrieve_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Retrieve("550e8400-e29b-41d4-a716-446655440000");

        Assert.NotNull(result);
        Assert.Equal("GET", handler.Request!.Method.Method);
        Assert.Equal("/templates/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Update_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Update("550e8400-e29b-41d4-a716-446655440000", new UpdateTemplateRequest
        {
            Name = "test-name",
            Subject = "test-subject",
            Html = "test-html",
            Mjml = "test-mjml",
            Text = "test-text",
            ChannelIds = ["550e8400-e29b-41d4-a716-446655440000"],
            Version = 10,
        });

        Assert.NotNull(result);
        Assert.Equal("PATCH", handler.Request!.Method.Method);
        Assert.Equal("/templates/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Delete_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        await client.Delete("550e8400-e29b-41d4-a716-446655440000");

        Assert.Equal("DELETE", handler.Request!.Method.Method);
        Assert.Equal("/templates/550e8400-e29b-41d4-a716-446655440000", handler.Request!.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task Render_SendsExpectedRequest()
    {
        var (client, handler) = CreateClient();

        var result = await client.Render("550e8400-e29b-41d4-a716-446655440000", new RenderTemplateRequest
        {
            InlineStyles = true,
        });

        Assert.NotNull(result);
        Assert.Equal("POST", handler.Request!.Method.Method);
        Assert.Equal("/templates/550e8400-e29b-41d4-a716-446655440000/render", handler.Request!.RequestUri!.AbsolutePath);
    }
}
