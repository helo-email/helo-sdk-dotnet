using System;
using System.Net;

namespace HeloEmail.Sdk.Errors
{
    public class ApiErrorException : Exception
    {
        public ApiErrorException(HttpStatusCode statusCode) : base(
            $"API call did not receive a successful response ({statusCode}).")
        {
            StatusCode = statusCode;
        }

        public ErrorResponse ErrorResponse { get; set; }
        public string ResponseContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}