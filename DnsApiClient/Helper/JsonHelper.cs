using System.Text.Json;

namespace ThwCalendarExporter.Helper;

public static class JsonHelper
{
    public static JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public static JsonSerializerOptions EnumOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}