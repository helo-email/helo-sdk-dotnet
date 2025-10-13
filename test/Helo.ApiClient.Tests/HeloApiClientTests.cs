using Helo.ApiClient.Models;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Helo.ApiClient.Tests;

public class HeloApiClientTests
{
    [Fact]
    public async Task Channels_Create()
    {
        var authProvider = new BaseBearerTokenAuthenticationProvider(new AccessTokenProvider());
        var adapter = new HttpClientRequestAdapter(authProvider);
        var client = new HeloApiClient(adapter);

        var response = await client.Channels.PostAsync(new CreateChannelRequest
        {
            Name = Guid.NewGuid().ToString(),
            DeliveryType = DeliveryType.Live,
        });

        Assert.NotNull(response.Id);
    }
}