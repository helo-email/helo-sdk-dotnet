using Microsoft.Kiota.Abstractions.Authentication;

namespace Helo.ApiClient.Tests;

public class AccessTokenProvider : IAccessTokenProvider
{
    public async Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = new())
    {
        await Task.CompletedTask;
        return "0199dedfda6b70a8bbc12caa362975bd_msLY5seoK3DSxPmpspMMTh9jpP0s7mkGh3pFIDoLj9Z6H4U2P8Fgm9H7ZMEsKobz";
    }

    public AllowedHostsValidator AllowedHostsValidator { get; } = new(["localhost:8000"]);
}