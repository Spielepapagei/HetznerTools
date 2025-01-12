using DnsTools.Commands.Config;
using DnsTools.Commands.Settings;
using DnsTools.Commands.Zone;
using DnsTools.Http;
using DnsTools.Services;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using ThwCalendarExporter.DependencyInjection;
using ThwCalendarExporter.Helper;

var services = new ServiceCollection();

services.AddSingleton<EnvironmentConfigService>();

//Http
services.AddSingleton<BaseHttpClient>();
services.AddSingleton<ZoneHttpClient>();

var app = new CommandApp(new TypeRegistrar(services));

app.Configure(config =>
{
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
    config.AddBranch<ConfigSettings>("config", add =>
    {
        add.AddBranch<ConfigSettings.TokenSettings>("token", add =>
        {
            add.AddCommand<GetTokenCommand>("get");
            add.AddCommand<SetTokenCommand>("set");
        });
    });
    
    config.AddBranch<ZoneSettings>("zones", add =>
    {
        add.SetDefaultCommand<ZonesCommand>();
        add.AddCommand<ZonesCommand>("get");
        add.AddCommand<CreateZoneCommand>("create");
    });
    
    config.SetExceptionHandler((ex, resolver) =>
    {
        if (ex.Message.Contains("Unknown command"))
        {
            AnsiConsole.MarkupLine($"{LogPrefixes.Error} {ex.Message}");
        }
        else
        { 
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
    });
});

return app.Run(args);

//dotnet .\DnsTools\bin\Release\net9.0\publish\DnsTools.dll