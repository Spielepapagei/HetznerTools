using System.Text.Json;
using DnsTools.Commands.Settings;
using DnsTools.Http;
using DnsTools.Models;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;

namespace DnsTools.Commands.Zone;

public class CreateZoneCommand : AsyncCommand<ZoneSettings.CreateZone>
{
    private readonly ZoneHttpClient Client;

    public CreateZoneCommand(ZoneHttpClient client)
    {
        Client = client;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ZoneSettings.CreateZone settings)
    {
        Validate(context, settings);

        var data = new CreateZoneRequest
        {
            Name = settings.ZoneName,
            Ttl = settings.Ttl
        };

        var response = await Client.CreateZone(data);
        
        AnsiConsole.MarkupLine(response.Message);

        if (response.Data != null)
        {
            AnsiConsole.Write(
                new Panel(new JsonText(JsonSerializer.Serialize(response.Data.Zone)))
                    .Header($"Zone {response.Data.Zone.Name}")
                    .Collapse()
                    .SquareBorder()
                    .BorderColor(Color.Yellow));
        }
        
        return 0;
    }

    public override ValidationResult Validate(CommandContext context, ZoneSettings.CreateZone settings)
    {
        if (settings.Ttl < 0) return ValidationResult.Error("TTL can not be negative");

        return ValidationResult.Success();
    }
}