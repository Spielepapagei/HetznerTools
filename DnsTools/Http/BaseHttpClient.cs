namespace DnsTools.Http;

public class BaseHttpClient
{
    public readonly HttpClient Client;

    public BaseHttpClient()
    {
        Client = new HttpClient();
        Client.BaseAddress = new Uri("https://dns.hetzner.com/api/v1/");
        Client.DefaultRequestHeaders.Add("Auth-API-Token", "<TOKEN>");
    }
}