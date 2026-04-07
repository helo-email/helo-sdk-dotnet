using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Helo.ApiClient.Errors;
using Microsoft.Extensions.Logging;

namespace Helo.ApiClient
{
    public class HeloBaseClient
    {
        private static readonly JsonSerializerOptions SerializerOptions = CreateSerializerOptions();
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public HeloBaseClient(HttpClient httpClient, ILogger logger)
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

        protected async Task<T> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<T>(content, SerializerOptions)
                : throw HandleError(content, response.StatusCode);
        }

        protected async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest body)
        {
            var requestContent = Serialize(body);
            var response = await _httpClient.PostAsync(url, requestContent);
            var content = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<TResponse>(content, SerializerOptions)
                : throw HandleError(content, response.StatusCode);
        }

        protected async Task<TResponse> Patch<TRequest, TResponse>(string url, TRequest body)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = Serialize(body),
            };
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<TResponse>(content, SerializerOptions)
                : throw HandleError(content, response.StatusCode);
        }

        protected async Task Delete(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw HandleError(content, response.StatusCode);
            }
        }

        private static StringContent Serialize<T>(T body) =>
            new StringContent(JsonSerializer.Serialize(body, SerializerOptions), Encoding.UTF8, "application/json");

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