using Helo.ApiClient.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Xunit.Abstractions;

namespace Helo.ApiClient.Tests;

public class SendingTests(ITestOutputHelper outputHelper)
{
    [Fact]
    public async Task SendBatch_InvalidFromDomain_ReturnsErrorResponse()
    {
        var client = GetClient();
        var exception = await Assert.ThrowsAsync<ErrorResponse>(async () =>
        {
            await client.Send.Transactional.Batch.PostAsync([
                new SendTransactionalRequest
                {
                    From = new MailAddress
                    {
                        Email = "test@blah.com",
                    },
                    To =
                    [
                        new MailAddress
                        {
                            Email = "test@helohq.com",
                        }
                    ],
                    Text = "Test Message",
                    Subject = "Test Message",
                }
            ], config => { config.Headers.Add("x-helo-channel-id", "241efbe3-3e50-4192-ab69-f8c9ccb10ae1"); });
        });
        Assert.Equal("DOMAIN_UNVERIFIED", exception.Code);
    }

    private HeloApiClient GetClient()
    {
        var handlers = KiotaClientFactory.CreateDefaultHandlers();
        handlers.Add(new LogResponseHandler(outputHelper));

        var httpMessageHandler =
            KiotaClientFactory.ChainHandlersCollectionAndGetFirstLink(
                KiotaClientFactory.GetDefaultHttpMessageHandler(),
                handlers.ToArray());

        var httpClient = new HttpClient(httpMessageHandler!);

        var authProvider = new BaseBearerTokenAuthenticationProvider(new AccessTokenProvider());
        var adapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        return new HeloApiClient(adapter);
    }
}

public class LogResponseHandler(ITestOutputHelper outputHelper) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        var respBody = await response.Content.ReadAsStringAsync(cancellationToken);
        outputHelper.WriteLine(respBody);
        return response;
    }
}