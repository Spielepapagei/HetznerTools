using static System.String;
using System.Text.Json.Serialization;

namespace DnsApiClient.Models;

public class DnsRecord
{
    public string ZoneId { get; set; } = Empty;
    public RecordType Type { get; set; }
    public string Id { get; set; } = Empty;
    public string Name { get; set; } = Empty;
    public string Value { get; set; } = Empty;
    public int Ttl { get; set; } = 86400;
    
    [JsonPropertyName("Created")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    [JsonPropertyName("modified")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
}