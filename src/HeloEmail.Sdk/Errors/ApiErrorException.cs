using System;
using System.Net;

namespace HeloEmail.Sdk.Errors
{
    /// <summary>
    /// Thrown for any non-2xx API response.
    /// </summary>
    public class ApiErrorException : Exception
    {
        public ApiErrorException(HttpStatusCode statusCode) : base(
            $"API call did not receive a successful response ({statusCode}).")
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// The parsed error body, or null when the response was not parseable.
        /// </summary>
        public ErrorResponse ErrorResponse { get; set; }

        /// <summary>
        /// The raw response body, always populated.
        /// </summary>
        public string ResponseContent { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
