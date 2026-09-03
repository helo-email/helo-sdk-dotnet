# Helo .NET SDK

Helo API

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
    })
    .AddAsKeyed();
```

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

## Configuration

`AddHelo` accepts an optional `baseUrl` to target a different API endpoint:

```csharp
services.AddHelo(apiKey, baseUrl: "https://api.helohq.com");
```

## Requirements

- netstandard2.0 or later

---

This SDK is generated from the Helo API OpenAPI description. Do not edit it by
hand — changes belong in the generator.
