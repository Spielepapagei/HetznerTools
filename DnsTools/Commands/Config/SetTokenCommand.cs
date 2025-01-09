using DnsTools.Commands.Settings;
using DnsTools.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using ThwCalendarExporter.Helper;

namespace DnsTools.Commands.Config;

public class SetTokenCommand : AsyncCommand<ConfigSettings.TokenSettings.SetToken>
{
    private readonly EnvironmentConfigService EnvConfigService;

    public SetTokenCommand(EnvironmentConfigService envConfigService)
    {
        EnvConfigService = envConfigService;
    }

    public async override Task<int> ExecuteAsync(CommandContext context, ConfigSettings.TokenSettings.SetToken settings)
    {
        var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter token:")
                .Secret());

        EnvConfigService.Get().Token = password;
        EnvConfigService.Save(EnvConfigService.Get());
        
        AnsiConsole.MarkupLine($"{LogPrefixes.Sucsess} Set new Token.");

        return 0;
    }
}