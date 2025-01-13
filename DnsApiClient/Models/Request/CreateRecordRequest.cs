using static System.String;

namespace DnsApiClient.Models.Request;

public class CreateRecordRequest
{
    public string ZoneId { get; set; } = Empty;
    public RecordType Type { get; set; }
    public string Name { get; set; } = Empty;
    public string Value { get; set; } = Empty;
    public int Ttl { get; set; } = 86400;
}