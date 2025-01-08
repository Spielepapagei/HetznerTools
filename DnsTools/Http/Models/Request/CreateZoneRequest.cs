namespace DnsTools.Models;

public class CreateZoneRequest
{
    public string Name { get; set; }
    public int Ttl { get; set; }
}