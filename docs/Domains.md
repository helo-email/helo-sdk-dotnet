# Domains

Register, verify, and manage domains for email sending.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**List**](#list) | **GET** /domains | List all domains |
| [**Create**](#create) | **POST** /domains | Create a domain |
| [**Retrieve**](#retrieve) | **GET** /domains/{id} | Retrieve a domain |
| [**Update**](#update) | **PATCH** /domains/{id} | Update a domain |
| [**Delete**](#delete) | **DELETE** /domains/{id} | Delete a domain |
| [**Verify**](#verify) | **POST** /domains/{id}/verify | Verify a domain |
| [**RotateKey**](#rotatekey) | **POST** /domains/{id}/rotate-key | Rotate a domain key |

## List

`GET /domains`

Retrieves all domains associated with the current account, including their verification status.

```csharp Domains_list
using HeloEmail.Sdk.Domains;

var paginatedResponseOfDomain = await helo.Domains.List(limit: 10, offset: 10);
```

## Create

`POST /domains`

Registers a new domain for sending emails. The domain must be verified before it can be used.

```csharp Domains_create
using HeloEmail.Sdk.Domains;

var domainWithDns = await helo.Domains.Create(new CreateDomainRequest
{
    Name = "test-name",
    ChannelIds = ["550e8400-e29b-41d4-a716-446655440000"],
});
```

## Retrieve

`GET /domains/{id}`

Gets detailed information about a specific domain, including verification status and configuration.

```csharp Domains_retrieve
using HeloEmail.Sdk.Domains;

var domainWithDns = await helo.Domains.Retrieve("550e8400-e29b-41d4-a716-446655440000");
```

## Update

`PATCH /domains/{id}`

Modifies an existing domain by ID.

```csharp Domains_update
using HeloEmail.Sdk.Domains;

var domain = await helo.Domains.Update("550e8400-e29b-41d4-a716-446655440000", new UpdateDomainRequest
{
    ChannelIds = ["550e8400-e29b-41d4-a716-446655440000"],
});
```

## Delete

`DELETE /domains/{id}`

Removes a domain from the account. This will stop all email sending from this domain.

```csharp Domains_delete
using HeloEmail.Sdk.Domains;

await helo.Domains.Delete("550e8400-e29b-41d4-a716-446655440000");
```

## Verify

`POST /domains/{id}/verify`

Initiates the domain verification process by checking DNS records.

```csharp Domains_verify
using HeloEmail.Sdk.Domains;

var dnsRecords = await helo.Domains.Verify("550e8400-e29b-41d4-a716-446655440000");
```

## RotateKey

`POST /domains/{id}/rotate-key`

Generates new DKIM keys for the domain. This is recommended for security best practices.

```csharp Domains_rotateKey
using HeloEmail.Sdk.Domains;

var dnsRecord = await helo.Domains.RotateKey("550e8400-e29b-41d4-a716-446655440000");
```
