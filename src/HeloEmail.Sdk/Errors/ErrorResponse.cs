using System.Collections.Generic;

namespace HeloEmail.Sdk.Errors
{
    /// <summary>
    /// Standard error response body.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// A URI reference identifying the problem type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Short, human-readable summary of the problem.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URI reference identifying the specific occurrence of the problem.
        /// </summary>
        public string Instance { get; set; }

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Application-specific error code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Human-readable explanation of this specific occurrence.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Unique request ID, useful for support correlation.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Field-level validation errors, keyed by field name.
        /// </summary>
        public Dictionary<string, List<ValidationError>> Errors { get; set; }
    }
}
