using DnsTools.Commands.Settings;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DnsTools.Commands.Zone;

public class UpdateZoneCommand : AsyncCommand<ZoneSettings.UpdateZone>
{
    public async override Task<int> ExecuteAsync(CommandContext context, ZoneSettings.UpdateZone settings)
    {
        return 0;
    }

    public override ValidationResult Validate(CommandContext context, ZoneSettings.UpdateZone settings)
    {
        return base.Validate(context, settings);
    }
}