# Domains

Manage sending domains and their DNS records. The examples assume an
`IHeloApiClient helo` — see the [README](../README.md) for how to register and inject it.

## Create a domain

`POST /domains`

```csharp Domains_create
using HeloEmail.Sdk.Domains;

var domain = await helo.Domains.Create(new CreateDomainRequest
{
    Name = "mail.example.com",
    ChannelIds = ["channel-id"],
});
```

## List domains

`GET /domains`

```csharp Domains_list
var domains = await helo.Domains.List(limit: 20);
```

## Retrieve a domain

`GET /domains/{id}`

```csharp Domains_retrieve
var domain = await helo.Domains.Retrieve("domain-id");
```

## Update a domain

`PATCH /domains/{id}`

```csharp Domains_update
using HeloEmail.Sdk.Domains;

var domain = await helo.Domains.Update("domain-id", new UpdateDomainRequest
{
    ChannelIds = ["channel-id"],
});
```

## Delete a domain

`DELETE /domains/{id}`

```csharp Domains_delete
await helo.Domains.Delete("domain-id");
```

## Verify a domain

`POST /domains/{id}/verify`

```csharp Domains_verify
var dnsRecords = await helo.Domains.Verify("domain-id");
```

## Rotate a domain's DKIM key

`POST /domains/{id}/rotate-key`

```csharp Domains_rotateKey
var dnsRecord = await helo.Domains.RotateKey("domain-id");
```
