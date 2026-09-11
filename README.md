# Helo .NET SDK

Helo Email API (https://helohq.com)

## Installation

```shell
dotnet add package HeloEmail.Sdk
```

## Quick start

### Dependency injection (ASP.NET Core)

Register the client in your `Program.cs` or `Startup.cs`:

```csharp
using HeloEmail.Sdk;

var apiKey = Environment.GetEnvironmentVariable("HELO_API_KEY");
builder.Services.AddHelo(apiKey);
```

Then inject `IHeloApiClient` wherever you need it:

```csharp
public class MyService(IHeloApiClient helo)
{
    public Task DoSomething() => helo.Activity.ListEvents();
}
```

If you want to supply your own `HttpClient`, call `AddHeloApiClients` and register a
named client under `KeyedServices.HeloApiClientName`:

```csharp
builder.Services.AddHeloApiClients();

builder.Services
    .AddHttpClient(KeyedServices.HeloApiClientName, c =>
    {
        c.BaseAddress = new Uri("https://api.helohq.com");
        c.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        c.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", SdkUserAgent.Value);
    })
    .AddAsKeyed();
```

`SdkUserAgent.Value` names the package, its version and the runtime it is on.
`AddHelo` sends it for you; send it here too so requests from a
self-configured client stay identifiable in the API logs.

## Available clients

`IHeloApiClient` exposes one property per API domain:

| Property | Documentation |
| -------- | ------------- |
| `Activity` | [docs/Activity.md](docs/Activity.md) |
| `Broadcasts` | [docs/Broadcasts.md](docs/Broadcasts.md) |
| `Channels` | [docs/Channels.md](docs/Channels.md) |
| `Domains` | [docs/Domains.md](docs/Domains.md) |
| `Sending` | [docs/Sending.md](docs/Sending.md) |
| `Statistics` | [docs/Statistics.md](docs/Statistics.md) |
| `Suppressions` | [docs/Suppressions.md](docs/Suppressions.md) |
| `Webhooks` | [docs/Webhooks.md](docs/Webhooks.md) |

## Error handling

Non-2xx responses throw `ApiErrorException`:

```csharp
using HeloEmail.Sdk.Errors;

try
{
    await helo.Activity.ListEvents();
}
catch (ApiErrorException ex)
{
    Console.WriteLine($"Status: {ex.StatusCode}");
    Console.WriteLine(ex.ResponseContent);
}
```

## Handling webhooks

`WebhookParser.Parse` reads the `eventType` off a raw webhook request body and deserializes it
into that event's payload type:

```csharp
using HeloEmail.Sdk.Webhooks;

var webhookPayload = WebhookParser.Parse(requestBody);
if (webhookPayload is MessageAcceptedWebhookPayload messageAccepted)
{
    Console.WriteLine($"message-accepted: {messageAccepted.MessageId}");
    return;
}
if (webhookPayload is EmailDeliveredWebhookPayload emailDelivered)
{
    Console.WriteLine($"email-delivered: {emailDelivered.Recipient}");
    return;
}
```

`Parse` returns `null` when the body carries no `eventType`, and throws
`ArgumentOutOfRangeException` for an event this SDK does not know about.

| Event | Payload |
| ----- | ------- |
| `message-accepted` | `MessageAcceptedWebhookPayload` |
| `message-processed` | `MessageProcessedWebhookPayload` |
| `email-delivered` | `EmailDeliveredWebhookPayload` |
| `email-bounced` | `EmailBouncedWebhookPayload` |
| `email-opened` | `EmailOpenedWebhookPayload` |
| `link-clicked` | `LinkClickedWebhookPayload` |
| `recipient-complained` | `RecipientComplainedWebhookPayload` |
| `recipient-unsubscribed` | `RecipientUnsubscribedWebhookPayload` |
| `recipient-resubscribed` | `RecipientResubscribedWebhookPayload` |
| `domain-key-verified` | `DomainKeyVerifiedPayload` |
| `domain-key-verification-failed` | `DomainKeyVerificationFailedPayload` |
| `return-path-domain-verified` | `ReturnPathDomainVerifiedPayload` |
| `return-path-domain-verification-failed` | `ReturnPathDomainVerificationFailedPayload` |

## Configuration

`AddHelo` accepts an optional `baseUrl` to target a different API endpoint:

```csharp
services.AddHelo(apiKey, baseUrl: "https://api.helohq.com");
```

## Requirements

- netstandard2.0 or later
