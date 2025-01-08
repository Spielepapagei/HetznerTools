using System.Net;
using System.Text.Json;
using DnsTools.Http.Models.Response;
using DnsTools.Models;
using ThwCalendarExporter.Helper;

namespace DnsTools.Http;

public class ZoneHttpClient
{
    private readonly BaseHttpClient Http;

    public ZoneHttpClient(BaseHttpClient http)
    {
        Http = http;
    }

    public async Task<ActionResponse<CreateZoneResponse>> CreateZone(CreateZoneRequest zone)
    {
        var result = new ActionResponse<CreateZoneResponse>();


        var rsp = await Http.Client.PostAsync(
            "zones",
            new StringContent(
                JsonSerializer.Serialize(zone)
            ));
        
        result.StatusCode = rsp.StatusCode;
        if (result.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"{LogPrefixes.Error} Server Responded with StatusCode {result.StatusCode.ToString()}";
            return result;
        }

        var json = await rsp.Content.ReadAsStringAsync();
        result.Data = JsonSerializer.Deserialize<CreateZoneResponse>(json, JsonHelper.DefaultOptions);
        result.Message = $"{LogPrefixes.Sucsess} {result.StatusCode}";
        return result;
    }
    
    public async Task<ActionResponse<GetZonesResponse>> GetZone(string? zoneName)
    {
        var result = new ActionResponse<GetZonesResponse>();

        var rsp = await Http.Client.GetAsync(
            $"zones{(zoneName != null ? $"/{zoneName}" : "")}");
        
        result.StatusCode = rsp.StatusCode;
        if (result.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"{LogPrefixes.Error} Server Responded with StatusCode {result.StatusCode.ToString()}";
            return result;
        }

        var json = await rsp.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<GetZonesResponse>(json, JsonHelper.DefaultOptions);
        if (data.Meta == null)
        {
            data.Zones.Add(JsonSerializer.Deserialize<CreateZoneResponse>(json, JsonHelper.DefaultOptions).Zone);
        } 
        
        result.Data = data;
        result.Message = $"{LogPrefixes.Sucsess} {result.StatusCode}";
        return result;
    }
}