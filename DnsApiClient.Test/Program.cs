using System.Data;
using DnsApiClient.Extensions;
using DnsApiClient.Test.Tests;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var serviceCollection = new ServiceCollection();

serviceCollection.AddHetznerDnsApi(configuration =>
{
    var token = Environment.GetEnvironmentVariable("Token", EnvironmentVariableTarget.User);
    if (token == null)
    {
        AnsiConsole.WriteLine("Token is Null!");
        throw new NoNullAllowedException();
    }
    configuration.Token = token;
});

serviceCollection.AddSingleton<ZoneTest>();
serviceCollection.AddSingleton<RecordTest>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var zoneTest = serviceProvider.GetRequiredService<ZoneTest>();
await zoneTest.Test();
