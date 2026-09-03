using System.Net;
using System.Text;

namespace HeloEmail.Sdk.Tests;

/// <summary>
/// Captures the outgoing request and replies with a canned success response, so the
/// generated tests can assert what each client sends without talking to a live API.
/// </summary>
public class StubHandler(HttpStatusCode statusCode = HttpStatusCode.OK, string body = "{}") : HttpMessageHandler
{
    public HttpRequestMessage? Request { get; private set; }
    public string? RequestBody { get; private set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        Request = request;
        if (request.Content != null)
            RequestBody = await request.Content.ReadAsStringAsync(cancellationToken);

        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json"),
        };
    }
}
