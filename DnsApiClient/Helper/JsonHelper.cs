using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ThwCalendarExporter.Helper;

public static class JsonHelper
{
    public static JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new Iso8601DateTimeConverter() }
    };

    public static JsonSerializerOptions ExportOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };
    
    public class Iso8601DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (dateString == null) return DateTime.MaxValue;

            var dateParts = dateString.Split(" ");

            //Format YYYY-MM-DDThh:mm:ss
            DateTime.TryParse(dateParts[0] + "T" + dateParts[1], out DateTime date);

            return date;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) {}
    }
}