# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this
repository.

## This repository is generated

Every file here except `.git` is produced by the `helo-sdk-generator-dotnet` gem, which lives in
the `helo-sdk-generator` repo (checked out next to this one) under
`vendor/gems/helo-sdk-generator-dotnet`. Regenerating sweeps the working tree first, so **any edit
made here is lost on the next run**. Fix the generator's Ruby or ERB templates instead, then
regenerate:

```bash
cd ../helo-sdk-generator && ./exe/helo-sdk-generator public
```

## Commands

```bash
dotnet build                                        # build the SDK and its tests
dotnet test                                         # run the generated tests (no server needed)
./scripts/publish.sh <version>                      # pack and push (requires helo_pkg_token)
```

## Architecture

- `HeloApiClient` / `IHeloApiClient` — facade exposing one domain client per API tag.
- One folder and sub-namespace per API tag (`ActivityClient` and
  friends), each client inheriting `BaseClient`.
- `BaseClient` — HTTP verbs, `System.Text.Json` serialization (web defaults, nulls omitted,
  kebab-case-lower enums), query-string building and error handling. Non-2xx responses throw
  `ApiErrorException`.
- `ServiceCollectionExtensions` — `AddHelo(apiKey, baseUrl)` registers the
  named `HttpClient` (`KeyedServices.HeloApiClientName`) plus every domain client.

Types shared by two or more domains live in the root `HeloEmail.Sdk` namespace; the rest sit in
their domain's sub-namespace, and error payloads in `HeloEmail.Sdk.Errors`.

## Documentation & code samples

`docs/*.md` holds one file per domain. Each example is a fenced block whose info string is the
operation's OpenAPI `operationId`:

    ```csharp Activity_listEvents
    ...
    ```

`helo-sdk-generator` extracts those blocks and embeds them into the published OpenAPI description
as the C# `x-codeSamples` entry for that operation, so the info string must stay an exact
`operationId`.
