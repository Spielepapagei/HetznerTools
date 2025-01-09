using DnsTools.Commands.Settings;
using DnsTools.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using ThwCalendarExporter.Helper;

namespace DnsTools.Commands.Config;

public class GetTokenCommand : AsyncCommand<ConfigSettings.TokenSettings.GetToken>
{
    private readonly EnvironmentConfigService EnvConfigService;

    public GetTokenCommand(EnvironmentConfigService envConfigService)
    {
        EnvConfigService = envConfigService;
    }

    public async override Task<int> ExecuteAsync(CommandContext context, ConfigSettings.TokenSettings.GetToken settings)
    {
        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>($"{LogPrefixes.Warning} Show Token?")
                .AddChoice(true)
                .AddChoice(false)
                .DefaultValue(false)
                .WithConverter(choice => choice ? "y" : "N"));

        if (confirmation)
            AnsiConsole.MarkupLine($"Your Token is {EnvConfigService.Get().Token}");
        else
            AnsiConsole.MarkupLine($"Aborted for safety reasons.");

        return 0;
    }
}