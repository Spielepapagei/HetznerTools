namespace DnsApiClient.Models.Response;

public class GetRecordResponse
{
    public DnsRecord Record { get; set; } = new();
}