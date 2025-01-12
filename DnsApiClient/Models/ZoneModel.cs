using static System.String;

namespace DnsApiClient.Models;

public class ZoneModel
{
    public string Id { get; set; } = Empty;
    public string Name { get; set; } = Empty;
    public long Ttl { get; set; }
    public string Registrar { get; set; } = Empty;
    public string LegacyDnsHost { get; set; } = Empty;
    public string[] LegacyNs { get; set; } = [];
    public string[] Ns { get; set; } = [];
    public string Created { get; set; } = Empty;
    public string Verified { get; set; } = Empty;
    public string Modified { get; set; } = Empty;
    public string Project { get; set; } = Empty;
    public string Owner { get; set; } = Empty;
    public string Permission { get; set; } = Empty;
    public ZoneTypeData ZoneType { get; set; } = new();
    public string Status { get; set; } = Empty;
    public bool Paused { get; set; }
    public bool IsSecondaryDns { get; set; }
    public TxtVerificationData TxtVerification { get; set; } = new();
    public long RecordsCount { get; set; }

    public class TxtVerificationData
    {
        public string Name { get; set; } = Empty;
        public string Token { get; set; } = Empty;
    }

    public class ZoneTypeData
    {
        public string Id { get; set; } = Empty;
        public string Name { get; set; } = Empty;
        public string Description { get; set; } = Empty;
        public object Prices { get; set; } = Empty;
    }
}