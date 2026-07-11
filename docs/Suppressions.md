# Suppressions

Manage suppressed recipients. The examples assume an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

## List suppressions

`GET /suppressions`

```csharp Suppressions_list
using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Suppressions;

var suppressions = await helo.Suppressions.List("channel-id", MailType.Transactional);
```

## Add suppressions

`POST /suppressions`

```csharp Suppressions_create
using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Suppressions;

var result = await helo.Suppressions.Create(new CreateSuppressionsRequest
{
    ChannelId = "channel-id",
    MailType = MailType.Transactional,
    Emails = ["blocked@example.com"],
});
```

## Remove suppressions

`POST /suppressions/remove`

```csharp Suppressions_remove
using HeloEmail.Sdk.Activity;
using HeloEmail.Sdk.Suppressions;

var result = await helo.Suppressions.Remove(new RemoveSuppressionsRequest
{
    ChannelId = "channel-id",
    MailType = MailType.Transactional,
    Emails = ["blocked@example.com"],
});
```
