namespace DnsApiClient.Models.Response;

public class GetRecordsResponse
{
    public DnsRecord Records { get; set; } = new();
    public MetaData Meta { get; set; } = new();
}