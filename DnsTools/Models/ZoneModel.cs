namespace DnsTools.Http.Models.Response;

public partial class ZoneModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public long Ttl { get; set; }
    public string Registrar { get; set; }
    public string LegacyDnsHost { get; set; }
    public string[] LegacyNs { get; set; }
    public string[] Ns { get; set; }
    public string Created { get; set; }
    public string Verified { get; set; }
    public string Modified { get; set; }
    public string Project { get; set; }
    public string Owner { get; set; }
    public string Permission { get; set; }
    public ZoneTypeData ZoneType { get; set; }
    public string Status { get; set; }
    public bool Paused { get; set; }
    public bool IsSecondaryDns { get; set; }
    public TxtVerificationData TxtVerification { get; set; }
    public long RecordsCount { get; set; }

    public class TxtVerificationData
    {
        public string Name { get; set; }
        public string Token { get; set; }
    }

    public class ZoneTypeData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object Prices { get; set; }
    }
}