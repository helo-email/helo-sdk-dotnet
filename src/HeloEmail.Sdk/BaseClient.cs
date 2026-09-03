using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HeloEmail.Sdk.Errors;
using Microsoft.Extensions.Logging;

namespace HeloEmail.Sdk
{
    /// <summary>
    /// HTTP, JSON and error-handling plumbing shared by every domain client.
    /// </summary>
    public class BaseClient
    {
        private static readonly JsonSerializerOptions SerializerOptions = CreateSerializerOptions();
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public BaseClient(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        private static JsonSerializerOptions CreateSerializerOptions()
        {
            var serializerOptions =
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                };
            serializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.KebabCaseLower));
            return serializerOptions;
        }

        protected Task<TResponse> Get<TResponse>(string url) =>
            Send<TResponse>(HttpMethod.Get, url);

        protected Task<TResponse> Post<TResponse>(string url, Dictionary<string, string> headers = null) =>
            Send<TResponse>(HttpMethod.Post, url, null, headers);

        protected Task<TResponse> Post<TRequest, TResponse>(string url, TRequest body,
            Dictionary<string, string> headers = null) =>
            Send<TResponse>(HttpMethod.Post, url, body, headers);

        protected Task<TResponse> Put<TRequest, TResponse>(string url, TRequest body,
            Dictionary<string, string> headers = null) =>
            Send<TResponse>(HttpMethod.Put, url, body, headers);

        protected Task<TResponse> Patch<TRequest, TResponse>(string url, TRequest body,
            Dictionary<string, string> headers = null) =>
            Send<TResponse>(PatchMethod, url, body, headers);

        protected Task Delete(string url) =>
            Send(HttpMethod.Delete, url);

        /// <summary>
        /// Issues a request and deserializes the response body.
        /// </summary>
        protected async Task<TResponse> Send<TResponse>(HttpMethod method, string url, object body = null,
            Dictionary<string, string> headers = null)
        {
            var response = await SendCore(method, url, body, headers);
            var content = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<TResponse>(content, SerializerOptions)
                : throw HandleError(content, response.StatusCode);
        }

        /// <summary>
        /// Issues a request whose response body is discarded.
        /// </summary>
        protected async Task Send(HttpMethod method, string url, object body = null,
            Dictionary<string, string> headers = null)
        {
            var response = await SendCore(method, url, body, headers);
            if (response.IsSuccessStatusCode) return;

            var content = await response.Content.ReadAsStringAsync();
            throw HandleError(content, response.StatusCode);
        }

        /// <summary>
        /// Appends the non-null parameters to <c>path</c> as a query string. Repeat-key
        /// parameters are passed as several tuples sharing the same key.
        /// </summary>
        protected static string BuildUrl(string path, List<(string Key, string Value)> parameters)
        {
            var sb = new StringBuilder(path);
            var first = true;
            foreach (var (key, value) in parameters)
            {
                if (value == null) continue;
                sb.Append(first ? '?' : '&');
                sb.Append($"{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}");
                first = false;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders an enum as the same wire value the JSON serializer would write,
        /// so query strings and request bodies agree.
        /// </summary>
        protected static string ToQueryValue<TEnum>(TEnum value) where TEnum : struct, Enum =>
            JsonNamingPolicy.KebabCaseLower.ConvertName(value.ToString());

        /// <summary>
        /// Renders an optional enum as its wire value, or null when unset so that
        /// <see cref="BuildUrl"/> drops the parameter.
        /// </summary>
        protected static string ToQueryValue<TEnum>(TEnum? value) where TEnum : struct, Enum =>
            value == null ? null : ToQueryValue(value.Value);

        private static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");

        private Task<HttpResponseMessage> SendCore(HttpMethod method, string url, object body,
            Dictionary<string, string> headers)
        {
            var request = new HttpRequestMessage(method, url);
            if (body != null)
                request.Content = Serialize(body);
            if (headers != null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

            return _httpClient.SendAsync(request);
        }

        private static StringContent Serialize(object body) =>
            new StringContent(JsonSerializer.Serialize(body, body.GetType(), SerializerOptions),
                Encoding.UTF8, "application/json");

        private ApiErrorException HandleError(string content, HttpStatusCode statusCode)
        {
            ErrorResponse errorResponse = null;
            try
            {
                errorResponse = JsonSerializer.Deserialize<ErrorResponse>(content, SerializerOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing error response");
            }

            return new ApiErrorException(statusCode)
            {
                ErrorResponse = errorResponse,
                ResponseContent = content,
            };
        }
    }
}
