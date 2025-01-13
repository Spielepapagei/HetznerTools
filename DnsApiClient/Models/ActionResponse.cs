using System.Net;
using Spectre.Console;

namespace DnsApiClient.Models;

public class ActionResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    
    public T TryGetData(string errorDetails = "")
    {
        if(Data == null)
        {
            if (string.IsNullOrEmpty(errorDetails))
                AnsiConsole.MarkupLine($"Error: {Message}");
            else
                AnsiConsole.MarkupLine($"{errorDetails}: {Message}");

            throw new Exception();
        }

        AnsiConsole.MarkupLine($"Success: {Message}");
        return Data;
    }
}