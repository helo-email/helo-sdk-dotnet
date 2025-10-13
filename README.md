# Generating the SDK

Assumes the latest OpenAPI spec is located in `../helo-web/vendor/schemas/helo.json`.

1. Install Kiota: `dotnet tool install --global Microsoft.OpenApi.Kiota`
2. Run `make generate`