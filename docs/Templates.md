# Templates

Manage templates for sending.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**List**](#list) | **GET** /templates |  |
| [**Create**](#create) | **POST** /templates |  |
| [**RenderDraft**](#renderdraft) | **POST** /templates/render |  |
| [**Retrieve**](#retrieve) | **GET** /templates/{id} |  |
| [**Update**](#update) | **PATCH** /templates/{id} |  |
| [**Delete**](#delete) | **DELETE** /templates/{id} |  |
| [**Render**](#render) | **POST** /templates/{id}/render |  |

## List

`GET /templates`

Fetch a paginated list of templates.

```csharp Templates_list
using HeloEmail.Sdk.Templates;

var paginatedResponseOfTemplateSummary = await helo.Templates.List(limit: 10, offset: 10);
```

## Create

`POST /templates`

Create a new template.

```csharp Templates_create
using HeloEmail.Sdk.Templates;

var template = await helo.Templates.Create(new CreateTemplateRequest
{
    Name = "test-name",
    Subject = "test-subject",
    Html = "test-html",
    Mjml = "test-mjml",
    Text = "test-text",
    ChannelIds = ["550e8400-e29b-41d4-a716-446655440000"],
});
```

## RenderDraft

`POST /templates/render`

Render ad-hoc template content (Subject/Html/Text) with sample data, without persisting anything.

```csharp Templates_renderDraft
using HeloEmail.Sdk.Templates;

var renderTemplate = await helo.Templates.RenderDraft(new RenderDraftRequest
{
    Subject = "test-subject",
    Html = "test-html",
    Mjml = "test-mjml",
    Text = "test-text",
    InlineStyles = true,
});
```

## Retrieve

`GET /templates/{id}`

Fetch a single template.

```csharp Templates_retrieve
using HeloEmail.Sdk.Templates;

var template = await helo.Templates.Retrieve("550e8400-e29b-41d4-a716-446655440000");
```

## Update

`PATCH /templates/{id}`

Update an existing template.

```csharp Templates_update
using HeloEmail.Sdk.Templates;

var template = await helo.Templates.Update("550e8400-e29b-41d4-a716-446655440000", new UpdateTemplateRequest
{
    Name = "test-name",
    Subject = "test-subject",
    Html = "test-html",
    Mjml = "test-mjml",
    Text = "test-text",
    ChannelIds = ["550e8400-e29b-41d4-a716-446655440000"],
    Version = 10,
});
```

## Delete

`DELETE /templates/{id}`

Delete a template.

```csharp Templates_delete
using HeloEmail.Sdk.Templates;

await helo.Templates.Delete("550e8400-e29b-41d4-a716-446655440000");
```

## Render

`POST /templates/{id}/render`

Render a saved template with sample data.

```csharp Templates_render
using HeloEmail.Sdk.Templates;

var renderTemplate = await helo.Templates.Render("550e8400-e29b-41d4-a716-446655440000", new RenderTemplateRequest
{
    InlineStyles = true,
});
```
