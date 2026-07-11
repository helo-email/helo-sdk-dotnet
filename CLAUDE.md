# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build

# Run tests (integration — see note below)
dotnet test

# Pack for NuGet
dotnet pack --configuration Release -p:PackageVersion=<version>

# Publish to GitHub Packages (requires helo_pkg_token env var)
./scripts/publish.sh <version>
```

Tests in `test/HeloEmail.Sdk.Tests/` are xUnit v3 **integration** tests, not unit tests.
`BaseFixture` points every client at `http://localhost:8000` with a bearer token from the
`HeloApiKey` environment variable, and each test calls the real API. They fail with
`Connection refused` unless a Helo API is running locally, so `dotnet test` will not pass
in isolation — build them (they compile with the SDK) but expect a local server to run them.

## Architecture

This is a manually-maintained .NET client SDK for the Helo email API, targeting `netstandard2.0`. It was migrated away from Kiota code generation to a hand-crafted implementation. (The `generate` target in the `Makefile` still invokes Kiota but is a leftover from that setup and is no longer used — the code is written by hand.)

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

**Namespaces:**

The root namespace is `HeloEmail.Sdk`; each domain has a sub-namespace matching its folder (e.g. `HeloEmail.Sdk.Channels`, `HeloEmail.Sdk.Sending`, `HeloEmail.Sdk.Suppressions`). Types shared across domains live at the root `HeloEmail.Sdk` level — `MailAddress`, `DeliveryType`, `Attachment`, `AttachmentDisposition` — while domain-specific types (requests, responses, per-domain enums) stay in their domain sub-namespace. A client that needs an enum owned by another domain imports it (e.g. `SuppressionsClient` reuses `HeloEmail.Sdk.Activity.MailType`).

**Query string building:**

`BaseClient.BuildUrl(path, List<(string Key, string Value)>)` skips null values and URL-encodes all keys and values. Repeat-key array params (e.g., `tags`) are added as multiple tuples with the same key. The `Statistics` client uses a bespoke `BuildUrl` overload with named parameters instead.

**Sending headers:**

The `SendingClient` maps the optional `channelId` and `idempotencyKey` parameters to `X-Helo-Channel-Id` and `X-Helo-Idempotency-Key` HTTP headers via `BaseClient.Post` — callers never deal with raw header names.

## Documentation & code samples

`docs/*.md` has one Markdown file per domain with a usage example for every operation. Each example is a fenced code block whose info string is the operation's OpenAPI `operationId`:

    ```csharp Channels_create
    ...
    ```

These are not only human docs. The external `helo-sdk-generator` extracts each block and embeds it into the published Helo OpenAPI spec as the C# entry under `x-codeSamples` for that operation. So the samples must be real, compilable calls against the current SDK surface, and each block's tag must be an exact spec `operationId`. When you add or change a domain method, update its `docs/` example — and add a block for any new operation — so the samples stay accurate.
