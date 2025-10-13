using Helo.ApiClient.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Helo.ApiClient.Tests;

public class SendingTests
{
    [Fact]
    public async Task FixMe()
    {
        var sendingToken = "Put something here";
        var authProvider = new BaseBearerTokenAuthenticationProvider(new AccessTokenProvider(sendingToken));
        var adapter = new HttpClientRequestAdapter(authProvider);
        var client = new HeloApiClient(adapter);

        var response = await client.Send.Transactional.Batch.PostAsync([
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
        ]);
        
        Assert.True(response.Count > 0);
    }
}