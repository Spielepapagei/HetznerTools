using static System.String;

namespace DnsApiClient.Models.Request;

public class CreateZoneRequest
{
    public string Name { get; set; } = Empty;
    public int Ttl { get; set; } = 86400;
}