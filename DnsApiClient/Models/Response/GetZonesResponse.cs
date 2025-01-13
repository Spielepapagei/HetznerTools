namespace DnsApiClient.Models.Response;

public partial class GetZonesResponse
{
    public List<ZoneModel> Zones { get; set; } = new();
    public MetaData Meta { get; set; } = new();
}