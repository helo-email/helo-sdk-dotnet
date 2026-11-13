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
        c.Timeout = Timeout.InfiniteTimeSpan; // RetryHandler times each attempt instead
    })
    .AddHttpMessageHandler(() => new RetryHandler())
    .AddAsKeyed();
```

Add `RetryHandler` as shown to get the SDK's automatic retries and per-attempt timeout on your
own client, and turn off `HttpClient.Timeout`, which would otherwise cover all the attempts together.

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
| `Templates` | [docs/Templates.md](docs/Templates.md) |
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

## Webhook signature verification

Webhook deliveries are signed with the endpoint's signing key. Verify every delivery before
acting on it, against the **raw** request body — parsing and re-serializing the JSON changes
the bytes and the signature will not match.

```csharp
using HeloEmail.Sdk.Webhooks;

[HttpPost("/webhooks/helo")]
public async Task<IActionResult> Receive()
{
    using var reader = new StreamReader(Request.Body);
    var rawBody = await reader.ReadToEndAsync(); // raw body, exactly as received

    try
    {
        WebhookSignature.Verify(
            Request.Headers["X-Helo-Webhook-Signature"],
            rawBody,
            Environment.GetEnvironmentVariable("HELO_WEBHOOK_SIGNING_KEY"));
    }
    catch (WebhookSignatureException)
    {
        return BadRequest();
    }

    // ... handle the event, then acknowledge quickly
    return NoContent();
}
```

`Verify` returns normally when the signature is valid and throws `WebhookSignatureException`
otherwise. Its `Error` says why, so a stale delivery can be treated differently from a
genuinely bad one:

| `Error` | Meaning |
| --- | --- |
| `MalformedHeader` | The header was not in the expected format |
| `UnsupportedVersion` | The delivery used a signing scheme this SDK version cannot verify — upgrade the package |
| `TimestampSkew` | Correctly signed, but too old to accept — possible replay, or clock drift |
| `SignatureMismatch` | Wrong signing key, or the body was modified in transit |

If you only want a boolean, use `WebhookSignature.IsValid` instead. Both accept the body as a
`string` or as raw `byte[]`.

The signature header may carry several versions at once (`t=...,v1=...,v2=...`) while a new
signing scheme is being rolled out. This SDK verifies against the newest version it supports
(`WebhookSignature.SupportedVersions`) and ignores elements it does not recognize, so a rollout
will not break this integration.

To compute a signature yourself — signing a fixture in tests, for example — use
`WebhookSignature.Generate(payload, signingKey, timestamp)`.

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

## Automatic retries

A request that fails with a network error, a timeout, or a `408`, `429`, `500`, `502`, `503` or
`504` response is retried up to 2 times, with exponential backoff and full jitter. It
is only retried when sending it again can't do anything twice: a `GET`, `PUT` or `DELETE`,
or a request carrying an idempotency key. A `429` is always retried, since the server
refused the request without acting on it. A `Retry-After` header is honoured; one asking
for more than 8 seconds is not retried early, and the error is returned instead.

Operations that take an idempotency key generate one for each call when you don't pass
one, and send it on every retry, so a send whose response was lost is never sent twice.
Pass your own key to make your own retries (a background job run again, say) safe too.

`AddHelo` takes `maxRetries` to change how many, or `0` to disable them. Retries are done by `RetryHandler`, a `DelegatingHandler` in the registered `HttpClient`'s pipeline. Each attempt times out after 30 seconds (`timeout` to change it) and is then retried like a network failure; `AddHelo` turns off `HttpClient.Timeout` so it doesn't cut the attempts short.

## Clearing a field

A null property isn't sent, so it leaves the field as it is. To clear a field the API accepts
`null` for, name the property in the request's `NullFields` (its doc comment lists which ones):

```csharp
var request = new UpdateTemplateRequest();
request.NullFields.Add(nameof(UpdateTemplateRequest.Text));
```

## Enums in responses

Every enum has an `Unknown` member, which a value added to the API after this SDK version was
released parses as, so an older SDK keeps working. `Unknown` can't be sent in a request.
`WebhookParser.Parse` likewise returns `null` for an event type the SDK doesn't know, so you can
acknowledge events you don't handle.

## Requirements

- netstandard2.0 or later
