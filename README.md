# Helo .NET SDK

Official .NET client library for the [Helo](https://helohq.com) email API.

## Installation

```shell
dotnet add package Helo.ApiClient
```

## Quick Start

### Dependency Injection (ASP.NET Core)

Register the Helo clients in your `Program.cs` or `Startup.cs`:

```csharp
builder.Services.AddHelo("your-api-key");
```

Then inject `IHeloApiClient` wherever you need it:

```csharp
public class EmailService(IHeloApiClient helo)
{
    public async Task SendWelcomeEmail(string toEmail, string toName)
    {
        var response = await helo.Sending.Transactional(new SendMessageRequest
        {
            From = new MailAddress { Email = "hello@yourapp.com", Name = "Your App" },
            To = [new MailAddress { Email = toEmail, Name = toName }],
            Subject = "Welcome!",
            Html = "<h1>Welcome aboard!</h1>",
        });
    }
}
```

If you want to use your own HttpClient, you can call `AddHeloApiClients` and inject your own HttpClient using the keyed service name (`KeyedServices.HeloApiClientName`):

```csharp
builder.Services.AddHeloApiClients();

builder.Services
    .AddHttpClient(KeyedServices.HeloApiClientName, c =>
    {
        c.BaseAddress = new Uri("https://api.helohq.com");
        c.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        // additional custom configuration
    })
    .AddAsKeyed();
```

### Standalone (without DI)

If you're not using a DI container, you can build the client manually using `IHttpClientFactory` or a plain `HttpClient`.

## Available Clients

`IHeloApiClient` exposes domain clients as properties:

| Property | Description |
|---|---|
| `Sending` | Send transactional emails and broadcasts |
| `Activity` | Query message activity and events |
| `Broadcasts` | Manage broadcast campaigns |
| `Channels` | Manage sending channels |
| `Domains` | Manage sending domains and DNS records |
| `Statistics` | Retrieve delivery statistics |
| `WebhookEndpoints` | Manage webhook endpoints |

## Sending Email

### Transactional

```csharp
var response = await helo.Sending.Transactional(new SendMessageRequest
{
    From = new MailAddress { Email = "sender@example.com", Name = "Sender" },
    To = [new MailAddress { Email = "recipient@example.com" }],
    Subject = "Hello from Helo",
    Html = "<p>Hello!</p>",
    Text = "Hello!",
    Tags = ["welcome", "onboarding"],
});
```

### With a Template

```csharp
var response = await helo.Sending.Transactional(new SendMessageRequest
{
    From = new MailAddress { Email = "sender@example.com" },
    To = [new MailAddress { Email = "recipient@example.com" }],
    Template = new MessageTemplate
    {
        Html = "<p>Hello {{name}}! Welcome to the {{plan}} plan!</p>",
        Data = new { name = "Alice", plan = "Pro" },
    },
});
```

### Batch

```csharp
var response = await helo.Sending.TransactionalBatch(new SendMessageBatchRequest
{
    Messages = [
        new SendMessageRequest { /* ... */ },
        new SendMessageRequest { /* ... */ },
    ],
});
```

### Optional Parameters

All send methods accept optional `channelId` and `idempotencyKey` parameters:

```csharp
await helo.Sending.Transactional(request, channelId: "chan_abc", idempotencyKey: "order-456");
```

## Error Handling

Non-2xx responses throw `ApiErrorException`:

```csharp
using Helo.ApiClient.Errors;

try
{
    await helo.Sending.Transactional(request);
}
catch (ApiErrorException ex)
{
    Console.WriteLine($"Status: {ex.StatusCode}");
    Console.WriteLine($"Error: {ex.ErrorResponse?.Message ?? ex.ResponseContent}");
}
```

## Configuration

`AddHelo` accepts an optional `baseUrl` to target a different API endpoint:

```csharp
services.AddHelo("your-api-key", baseUrl: "https://custom.api.helohq.com");
```

## Requirements

- .NET Standard 2.0 or later (.NET 6+, .NET Framework 4.6.1+)
