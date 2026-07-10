namespace HeloEmail.Sdk.Domains
{
    public class DnsRecordResponse
    {
        public DnsRecordType Type { get; set; }
        public string Host { get; set; }
        public string Value { get; set; }
        public DnsRecordStatus Status { get; set; }
    }
}
