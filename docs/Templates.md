# Templates

Manage templates for sending.

The examples below assume you have an `IHeloApiClient helo` — see the
[README](../README.md) for how to register and inject it.

| Method | HTTP request | Description |
| ------ | ------------ | ----------- |
| [**List**](#list) | **GET** /templates | List all templates |
| [**Create**](#create) | **POST** /templates | Create a template |
| [**RenderDraft**](#renderdraft) | **POST** /templates/render | Render template content |
| [**Retrieve**](#retrieve) | **GET** /templates/{id} | Retrieve a template |
| [**Update**](#update) | **PATCH** /templates/{id} | Update a template |
| [**Delete**](#delete) | **DELETE** /templates/{id} | Delete a template |
| [**Render**](#render) | **POST** /templates/{id}/render | Render a template |

## List

`GET /templates`

Retrieves a paginated list of templates, optionally filtered by name or channel.

```csharp Templates_list
using HeloEmail.Sdk.Templates;

var paginatedResponseOfTemplateSummary = await helo.Templates.List(limit: 10, offset: 10);
```

## Create

`POST /templates`

Creates a new template from HTML, MJML, or plain-text content. MJML is compiled to HTML when the template is saved.

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

Renders unsaved template content (subject, HTML or MJML, and text) with sample data, without persisting anything.

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

Fetches the details and content of a specific template.

```csharp Templates_retrieve
using HeloEmail.Sdk.Templates;

var template = await helo.Templates.Retrieve("550e8400-e29b-41d4-a716-446655440000");
```

## Update

`PATCH /templates/{id}`

Modifies an existing template. Only the provided fields are changed.

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

Permanently removes a template. This action cannot be undone.

```csharp Templates_delete
using HeloEmail.Sdk.Templates;

await helo.Templates.Delete("550e8400-e29b-41d4-a716-446655440000");
```

## Render

`POST /templates/{id}/render`

Renders a saved template with sample data, to preview the output before sending.

```csharp Templates_render
using HeloEmail.Sdk.Templates;

var renderTemplate = await helo.Templates.Render("550e8400-e29b-41d4-a716-446655440000", new RenderTemplateRequest
{
    InlineStyles = true,
});
```
