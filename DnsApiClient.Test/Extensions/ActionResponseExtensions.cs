using DnsApiClient.Models;
using Spectre.Console;

namespace DnsApiClient.Test.Extensions;

public static class ActionResponseExtensions
{
    public static T TryGetData<T>(this ActionResponse<T> response, string errorDetails = "")
    {
        if(response.Data == null)
        {
            if (string.IsNullOrEmpty(errorDetails))
                AnsiConsole.MarkupLine($"Error: {response.Message}");
            else
                AnsiConsole.MarkupLine($"{errorDetails}: {response.Message}");

            throw new Exception();
        }

        AnsiConsole.MarkupLine($"Success: {response.Message}");
        return response.Data;
    }
}