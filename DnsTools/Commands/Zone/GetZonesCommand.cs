using System.Text.Json;
using DnsTools.Commands.Settings;
using DnsTools.Http;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;

namespace DnsTools.Commands.Zone;

public class GetZonesCommand : AsyncCommand<ZoneSettings.GetZones>
{
    private readonly ZoneHttpClient Client;

    public GetZonesCommand(ZoneHttpClient client)
    {
        Client = client;
    }
    
    public override async Task<int> ExecuteAsync(CommandContext context, ZoneSettings.GetZones settings)
    {
        Validate(context, settings);

        var response = await Client.GetZone(settings.ZoneName);
        
        AnsiConsole.MarkupLine(response.Message);

        if (response.Data != null)
        {
            AnsiConsole.Write(
                new Panel(new JsonText(JsonSerializer.Serialize(response.Data.Zones)))
                    .Header($"Zones")
                    .Collapse()
                    .SquareBorder()
                    .BorderColor(Color.Yellow));
        }
        
        return 0;
    }
}