using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging.Abstractions;
using HeloEmail.Sdk.Templates;

namespace HeloEmail.Sdk.Tests;

public class SerializationTests
{
    // A value the API added after this SDK version was released must not break parsing.
    [Fact]
    public void AnEnumReadsAValueItDoesNotKnowAsUnknown()
    {
        Assert.Equal(AttachmentDisposition.Unknown, JsonSerializer.Deserialize<AttachmentDisposition>("\"added-later\""));
        Assert.Equal(AttachmentDisposition.Attachment, JsonSerializer.Deserialize<AttachmentDisposition>("\"attachment\""));
    }

    [Fact]
    public void AnEnumWritesItsWireValueAndRefusesToWriteUnknown()
    {
        Assert.Equal("\"attachment\"", JsonSerializer.Serialize(AttachmentDisposition.Attachment));
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(AttachmentDisposition.Unknown));
    }

    /// <summary>Sends a body through BaseClient's serializer, as every client does.</summary>
    private class TestClient(HttpClient httpClient) : BaseClient(httpClient, NullLogger.Instance)
    {
        public Task Send(object body) => Send(HttpMethod.Post, "/things", body);
    }

    private static (TestClient Client, StubHandler Handler) CreateClient()
    {
        var handler = new StubHandler(HttpStatusCode.NoContent, "");
        return (new TestClient(new HttpClient(handler) { BaseAddress = new Uri("https://api.example.test") }), handler);
    }

    // Leaving a property null can't clear a field, so the ones to clear are named.
    [Fact]
    public async Task NullFieldsSendsTheNamedPropertiesAsNull()
    {
        var (client, handler) = CreateClient();

        var request = new UpdateTemplateRequest();
        request.NullFields.Add(nameof(UpdateTemplateRequest.Html));
        await client.Send(request);

        var body = JsonDocument.Parse(handler.RequestBody!).RootElement;
        Assert.Equal(JsonValueKind.Null, body.GetProperty("html").ValueKind);
    }

    [Fact]
    public async Task NullFieldsRejectsAPropertyTheApiCannotClear()
    {
        var (client, _) = CreateClient();

        var request = new UpdateTemplateRequest();
        request.NullFields.Add("NotAField");
        var error = await Assert.ThrowsAsync<ArgumentException>(() => client.Send(request));
        Assert.Contains("can't be null", error.Message);
    }
}
