using System.Collections.Generic;

namespace HeloEmail.Sdk.Errors
{
    public class ErrorResponse
    {
        public ErrorResponse(string type,
            string title,
            string instance,
            int status,
            string code,
            string detail,
            string requestId,
            Dictionary<string, ValidationError[]> errors = null)
        {
            Type = type;
            Title = title;
            Instance = instance;
            Status = status;
            Code = code;
            Detail = detail;
            RequestId = requestId;
            Errors = errors;
        }

        public string Type { get; }
        public string Title { get; }
        public string Instance { get; }
        public int Status { get; }
        public string Code { get; }
        public string Detail { get; }
        public string RequestId { get; }
        public Dictionary<string, ValidationError[]> Errors { get; }
    }

    public class ValidationError
    {
        public ValidationError(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }
        public string Code { get; }
    }
}