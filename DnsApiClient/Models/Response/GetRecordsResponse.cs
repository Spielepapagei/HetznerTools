namespace DnsApiClient.Models.Response;

public class GetRecordsResponse
{
    public DnsRecord[] Records { get; set; } = [];
    public MetaData Meta { get; set; } = new();
}