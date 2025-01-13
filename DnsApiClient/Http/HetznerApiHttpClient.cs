namespace DnsApiClient.Http;

public class HetznerApiHttpClient
{
    public readonly HttpClient Client;
    public readonly HttpClientConfiguration Configuration;

    public HetznerApiHttpClient(HttpClientConfiguration configuration)
    {
        Configuration = configuration;
        
        Client = new HttpClient();
        Client.BaseAddress = new Uri("https://dns.hetzner.com/api/v1/");
        Client.DefaultRequestHeaders.Add("Auth-API-Token", Configuration.Token);
    }
}