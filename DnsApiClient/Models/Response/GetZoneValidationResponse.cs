using static System.String;

namespace DnsApiClient.Models.Response;

public class GetZoneValidationResponse
{
    public string ParsedRecords { get; set; } = Empty;
    public ErrorData Error { get; set; } = new();
    
    public class ErrorData
    {
        public string Message { get; set; } = Empty;
        public long Code { get; set; }
    }
    
}