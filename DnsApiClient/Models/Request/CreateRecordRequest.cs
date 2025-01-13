using System.Text.Json.Serialization;
using static System.String;

namespace DnsApiClient.Models.Request;

public class CreateRecordRequest
{
    [JsonPropertyName("zone_id")]
    public string ZoneId { get; set; } = Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RecordType Type { get; set; }
    public string Name { get; set; } = Empty;
    public string Value { get; set; } = Empty;
    public int Ttl { get; set; } = 86400;
}