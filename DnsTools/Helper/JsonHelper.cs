using System.Text.Json;

namespace ThwCalendarExporter.Helper;

public static class JsonHelper
{
    public static JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}