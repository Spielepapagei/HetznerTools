using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DnsApiClient.Helper;
using DnsApiClient.Models;
using DnsApiClient.Models.Request;
using DnsApiClient.Models.Response;

namespace DnsApiClient.Http;

public class RecordsClient
{
    public readonly HetznerApiHttpClient Http;

    public RecordsClient(HetznerApiHttpClient http)
    {
        Http = http;
    }
    
    #region GetAllRecords
    public async Task<ActionResponse<GetRecordsResponse>> GetAllRecords(string? zoneId = null, int? page = null, int? pageSize = null)
    {
        var result = new ActionResponse<GetRecordsResponse>();
        
        //Add Parameters
        var reqArgs = $"?zone_id={zoneId}";
        if (page >= 1)
        {
            result.Message = "Page must match '>= 1'";
            result.StatusCode = HttpStatusCode.BadRequest;
            return result;
        }
        if(page != null) reqArgs += "&page=" + page;
        
        if (pageSize >= 1)
        {
            result.Message = "PageSize must match '>= 1'";
            result.StatusCode = HttpStatusCode.BadRequest;
            return result;
        }
        if(pageSize != null) reqArgs += "&per_page=" + pageSize;

        
        //Api Interaction
        var zoneResponse = await Http.Client.GetAsync($"records?{reqArgs}");
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }    
        
        //Process Result
        var records = await zoneResponse.Content.ReadFromJsonAsync<GetRecordsResponse>(JsonHelper.DefaultOptions);
        if (records == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully got records from server.";
        result.Data = records;
        return result;
    }
    #endregion
    
    #region GetRecord
    public async Task<ActionResponse<GetRecordResponse>> GetRecord(string recordId)
    {
        var result = new ActionResponse<GetRecordResponse>();
        
        //Api Interaction
        var zoneResponse = await Http.Client.GetAsync($"records/{recordId}");
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }    
        
        //Process Result
        var records = await zoneResponse.Content.ReadFromJsonAsync<GetRecordResponse>(JsonHelper.DefaultOptions);
        if (records == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully got record from server.";
        result.Data = records;
        return result;
    }
    #endregion
    
    #region CreateRecord
    public async Task<ActionResponse<GetRecordResponse>> CreateRecord(CreateRecordRequest data)
    {
        var result = new ActionResponse<GetRecordResponse>();
        var content = new StringContent(JsonSerializer.Serialize(data, JsonHelper.ExportOptions));
        
        //Api Interaction
        var zoneResponse = await Http.Client.PostAsync(
            "records",
            content);
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }    
        
        //Process Result
        var record = await zoneResponse.Content.ReadFromJsonAsync<GetRecordResponse>(JsonHelper.DefaultOptions);
        if (record == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully created record on server.";
        result.Data = record;
        return result;
    }
    #endregion
    
    #region UpdateRecord
    public async Task<ActionResponse<GetRecordResponse>> UpdateRecord(string recordId, CreateRecordRequest data)
    {
        var result = new ActionResponse<GetRecordResponse>();
        
        //Api Interaction
        var zoneResponse = await Http.Client.PutAsync($"records/{recordId}",
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
        var records = await zoneResponse.Content.ReadFromJsonAsync<GetRecordResponse>(JsonHelper.DefaultOptions);
        if (records == null)
        {
            result.Message = "Bad json data.";
            return result;
        }

        //Return Result
        result.Message = "Successfully updated record on server.";
        result.Data = records;
        return result;
    }
    #endregion
    
    #region DeleteRecord
    public async Task<ActionResponse<string>> DeleteRecord(string recordId)
    {
        var result = new ActionResponse<string>();
        
        //Api Interaction
        var zoneResponse = await Http.Client.DeleteAsync($"records/{recordId}");
        result.StatusCode = zoneResponse.StatusCode;
        if (zoneResponse.StatusCode != HttpStatusCode.OK)
        {
            result.Message = $"Server responded with: {result.StatusCode}";
            return result;
        }

        //Return Result
        result.Message = "Successfully deleted record from server.";
        result.Data = recordId;
        return result;
    }
    #endregion
}