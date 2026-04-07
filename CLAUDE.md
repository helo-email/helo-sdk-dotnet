# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build

# Pack for NuGet
dotnet pack --configuration Release -p:PackageVersion=<version>

# Publish to GitHub Packages (requires helo_pkg_token env var)
./scripts/publish.sh <version>
```

There are currently no tests (the test project was removed during a rewrite).

## Architecture

This is a manually-maintained .NET client SDK for the Helo email API, targeting `netstandard2.0`. It was migrated away from Kiota code generation to a hand-crafted implementation.

**Layer structure:**

- `HeloApiClient` / `IHeloApiClient` — Top-level facade that composes all domain clients as properties. New API domains get added as properties here and wired up in `ServiceCollectionExtensions`.
- Domain clients (e.g., `StatisticsClient`, `ActivityClient`) — One subfolder per domain. Each client inherits `BaseClient` and calls its protected HTTP methods.
- `BaseClient` — Handles HTTP execution, JSON deserialization, error handling, and query string building. Add new HTTP verbs here, not in individual clients.
- `ServiceCollectionExtensions` — Two-step DI registration: `AddHeloHttpClient(baseUrl?)` creates the named `HttpClient` (default base URL: `https://api.helohq.com`), then `AddHeloApiClients()` wires up all domain clients as transient. `IHeloApiClient` is also registered as transient.

**Naming conventions:**

- Domain client interfaces drop the `Helo` prefix: `IActivityClient`, `IChannelsClient`, etc.
- The top-level client retains it: `IHeloApiClient` / `HeloApiClient`.

**Error handling:**

Non-2xx responses are thrown as `ApiErrorException`, which carries the HTTP status code, a deserialized `ErrorResponse` (if parseable), and the raw response body as a fallback.

**JSON serialization:**

`System.Text.Json` with web defaults, `WhenWritingNull` ignore condition, and a kebab-case-lower enum converter. Options are created once as a static field on `BaseClient`. All enums must be single-word or hyphenated values to round-trip correctly with this converter.

**DI wiring:**

The named `HttpClient` (`KeyedServices.HeloApiClientName = "helo-api"`) is registered via `IHttpClientFactory` and injected into domain clients as a `[FromKeyedServices]` constructor parameter.

**Shared types (root namespace `Helo.ApiClient`):**

Types used across multiple domains live at the root level: `MailAddress`, `DeliveryType`, `Attachment`, `AttachmentDisposition`. Domain-specific types stay in their namespace subfolder.

**Query string building:**

`BaseClient.BuildUrl(path, List<(string Key, string Value)>)` skips null values and URL-encodes all keys and values. Repeat-key array params (e.g., `tags`) are added as multiple tuples with the same key. The `Statistics` client uses a bespoke `BuildUrl` overload with named parameters instead.

**Sending headers:**

The `SendingClient` maps the optional `channelId` and `idempotencyKey` parameters to `X-Helo-Channel-Id` and `X-Helo-Idempotency-Key` HTTP headers via `BaseClient.Post` — callers never deal with raw header names.
