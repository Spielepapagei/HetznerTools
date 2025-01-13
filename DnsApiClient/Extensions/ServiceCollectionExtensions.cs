using DnsApiClient.Configuration;
using DnsApiClient.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DnsApiClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddHetznerDnsApi(this IServiceCollection collection, Action<HttpClientConfiguration> onConfigure)
    {
        var configuration = new HttpClientConfiguration();
        onConfigure.Invoke(configuration);
        collection.AddSingleton(configuration);
        
        //Add Clients
        collection.AddSingleton<HetznerApiHttpClient>(); 
        collection.AddSingleton<ZonesClient>();
        collection.AddSingleton<RecordsClient>();
    }

}