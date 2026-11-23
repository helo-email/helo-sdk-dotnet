using System.Collections.Generic;
using HeloEmail.Sdk.Errors;

namespace HeloEmail.Sdk.Sending
{
    public class SendMessageFailedResponse
    {
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Field-level validation errors for this message, keyed by field (relative to the message). Only present when `errorCode` is `validation_failed`, which happens in a `relaxed` batch.
        /// </summary>
        public Dictionary<string, List<ValidationError>> Errors { get; set; }
    }
}
