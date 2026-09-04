# Suppressions

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**List**](#list) | **GET** /suppressions | List suppressions |
| [**Create**](#create) | **POST** /suppressions | Create suppressions |
| [**Remove**](#remove) | **POST** /suppressions/remove | Remove suppressions |

## List

`GET /suppressions`

Retrieves a list of suppressed email addresses for a channel.

```csharp Suppressions_list
using HeloEmail.Sdk;
using HeloEmail.Sdk.Suppressions;

var paginatedResponseOfSuppression = await helo.Suppressions.List("550e8400-e29b-41d4-a716-446655440000", MailType.Transactional, reason: SuppressionReason.Bounce, email: "test@example.com");
```

## Create

`POST /suppressions`

Adds email addresses to the suppression list to prevent future sends.

```csharp Suppressions_create
using HeloEmail.Sdk;
using HeloEmail.Sdk.Suppressions;

var createSuppressions = await helo.Suppressions.Create(new CreateSuppressionsRequest
{
    ChannelId = "550e8400-e29b-41d4-a716-446655440000",
    MailType = MailType.Transactional,
    Emails = ["test@example.com"],
});
```

## Remove

`POST /suppressions/remove`

Removes email addresses from the suppression list to allow future sends.

```csharp Suppressions_remove
using HeloEmail.Sdk;
using HeloEmail.Sdk.Suppressions;

var removeSuppressions = await helo.Suppressions.Remove(new RemoveSuppressionsRequest
{
    ChannelId = "550e8400-e29b-41d4-a716-446655440000",
    MailType = MailType.Transactional,
    Emails = ["test@example.com"],
});
```
