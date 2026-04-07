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

- `HeloApiClient` / `IHeloApiClient` — Top-level facade that composes domain-specific service clients. New API domains get added as properties here.
- Service clients (e.g., `HeloStatisticsClient`) — Domain-scoped clients that inherit `HeloBaseClient` and call the HTTP methods it provides.
- `HeloBaseClient` — Handles HTTP execution, JSON deserialization, and error handling. Currently exposes `Get<T>(url)`. New HTTP verbs (POST, PUT, etc.) should be added here.
- `ServiceCollectionExtensions` — Two-step DI registration: `RegisterHeloHttpClient(baseUrl)` creates the named `HttpClient`, then `RegisterHeloApiClients(baseUrl)` wires up service clients as transient.

**Error handling:**

Non-2xx responses are thrown as `ApiErrorException`, which carries the HTTP status code, a deserialized `ErrorResponse` (if parseable), and the raw response body as a fallback.

**JSON serialization:**

`System.Text.Json` with web defaults, `WhenWritingNull` ignore condition, and a kebab-case-lower enum converter. Options are created once as a static field on `HeloBaseClient`.

**DI wiring:**

The named `HttpClient` (`KeyedServices.HeloApiClientName = "helo-api"`) is registered via `IHttpClientFactory` and injected into service clients as a keyed service. When adding a new service client, register it in `ServiceCollectionExtensions.RegisterHeloApiClients`.
