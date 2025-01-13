using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DnsApiClient.Models;
using DnsApiClient.Models.Request;
using DnsApiClient.Models.Response;
using ThwCalendarExporter.Helper;

namespace DnsApiClient.Http;

public class ZonesClient
{
    public readonly HetznerApiHttpClient Http;

    public ZonesClient(HetznerApiHttpClient http)
    {
        Http = http;
    }
    
    #region GetZone
    public async Task<ActionResponse<ZoneModel>> GetZone(string zoneId)
    {
        var result = new ActionResponse<ZoneModel>();
        
        //Api Interaction
        var zoneResponse = await Http.Client.GetAsync($"zones/{zoneId}");
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode} ";
            return result;
        }    
        
        //Process Result
        var zone = await zoneResponse.Content.ReadFromJsonAsync<GetZoneResponse>(JsonHelper.DefaultOptions);
        if (zone == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully got zone from Server.";
        result.Data = zone.Zone;
        return result;
    }
    #endregion
    
    #region GetZones
    public async Task<ActionResponse<GetZonesResponse>> GetZones()
    {
        var result = new ActionResponse<GetZonesResponse>();
        
        //Api Interaction
        var zoneResponse = await Http.Client.GetAsync("zones");
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode} ";
            return result;
        }    
        
        //Process Result
        var zones = await zoneResponse.Content.ReadFromJsonAsync<GetZonesResponse>(JsonHelper.DefaultOptions);
        if (zones == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully got zones from Server.";
        result.Data = zones;
        return result;
    }
    #endregion
    
    #region CreateZone
    public async Task<ActionResponse<ZoneModel>> CreateZone(CreateZoneRequest data)
    {
        var result = new ActionResponse<ZoneModel>();
        data.Name = data.Name.ToLower();
        
        //Api Interaction
        var zoneResponse = await Http.Client.PostAsync(
            "zones",
            new StringContent(
                JsonSerializer.Serialize(data)
            ));
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }    
        
        //Process Result
        var zone = await zoneResponse.Content.ReadFromJsonAsync<GetZoneResponse>(JsonHelper.DefaultOptions);
        if (zone == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully created zone on Server.";
        result.Data = zone.Zone;
        return result;
    }
    #endregion
    
    #region UpdateZone
    public async Task<ActionResponse<ZoneModel>> UpdateZone(string zoneId, CreateZoneRequest data)
    {
        var result = new ActionResponse<ZoneModel>();
        data.Name = data.Name.ToLower();
        
        //Api Interaction
        var zoneResponse = await Http.Client.PutAsync(
            $"zones/{zoneId}",
            new StringContent(
                JsonSerializer.Serialize(data)
            ));
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }    
        
        //Process Result
        var zone = await zoneResponse.Content.ReadFromJsonAsync<GetZoneResponse>(JsonHelper.DefaultOptions);
        if (zone == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully updated zone on Server.";
        result.Data = zone.Zone;
        return result;
    }
    #endregion
    
    #region DeleteZone
    public async Task<ActionResponse<string>> DeleteZone(string zoneId)
    {
        var result = new ActionResponse<string>();
        
        //Api Interaction
        var zoneResponse = await Http.Client.DeleteAsync(
            $"zones/{zoneId}");
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }    

        //Return Result
        result.Message = "Successfully deleted zone on Server.";
        result.Data = zoneId;
        return result;
    }
    #endregion
    
    
    
    #region ImportZoneFile
    public async Task<ActionResponse<ZoneModel>> ImportZoneFile(string zoneId, string zoneFile)
    {
        var result = new ActionResponse<ZoneModel>();
        
        //Api Interaction
        var zoneResponse = await Http.Client.PostAsync(
            $"zones/{zoneId}/import",
            new StringContent(
                zoneFile
            ));
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }    
        
        //Process Result
        var zone = await zoneResponse.Content.ReadFromJsonAsync<GetZoneResponse>(JsonHelper.DefaultOptions);
        if (zone == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully uploaded zoneFile on Server.";
        result.Data = zone.Zone;
        return result;
    }
    #endregion
    
    #region ExportZoneFile
    public async Task<ActionResponse<string>> ExportZoneFile(string zoneId)
    {
        var result = new ActionResponse<string>();
        
        //Api Interaction
        var zoneResponse = await Http.Client.GetAsync(
            $"zones/{zoneId}/export");
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }
        
        //Process Result
        var zoneFile = await zoneResponse.Content.ReadAsStringAsync();

        //Return Result
        result.Message = "Successfully got zoneFile from Server.";
        result.Data = zoneFile;
        return result;
    }
    #endregion
    
    #region ValidateZoneFile
    public async Task<ActionResponse<GetZoneValidationResponse>> ValidateZoneFile(string zoneFileAsString)
    {
        var result = new ActionResponse<GetZoneValidationResponse>();
        var fixedZoneFileAsString = zoneFileAsString.Replace("\r", "");
        var content = new StringContent(fixedZoneFileAsString, Encoding.UTF8, "text/plain");
        
        //Api Interaction
        var zoneResponse = await Http.Client.PostAsync(
            $"zones/file/validate",
            content);
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }
        
        //Process Result
        var zoneValidation = await zoneResponse.Content.ReadFromJsonAsync<GetZoneValidationResponse>(JsonHelper.DefaultOptions);
        if (zoneValidation == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully validated zoneFile.";
        result.Data = zoneValidation;
        return result;
    }
    #endregion
    
}