using DnsTools.Commands.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DnsTools.Commands.Zone;

public class DeleteZoneCommand : AsyncCommand<ZoneSettings.DeleteZone>
{
    public async override Task<int> ExecuteAsync(CommandContext context, ZoneSettings.DeleteZone settings)
    {
        return 0;
    }

    public override ValidationResult Validate(CommandContext context, ZoneSettings.DeleteZone settings)
    {
        return base.Validate(context, settings);
    }
}