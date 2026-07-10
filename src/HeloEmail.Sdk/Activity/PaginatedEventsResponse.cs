using System.Collections.Generic;

namespace HeloEmail.Sdk.Activity
{
    public class PaginatedEventsResponse
    {
        public long? After { get; set; }
        public double TotalCount { get; set; }
        public List<ActivityEvent> Results { get; set; }
    }
}
