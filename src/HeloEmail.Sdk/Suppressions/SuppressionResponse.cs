using System;

namespace HeloEmail.Sdk.Suppressions
{
    public class SuppressionResponse
    {
        public string Email { get; set; }
        public SuppressionReason Reason { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
