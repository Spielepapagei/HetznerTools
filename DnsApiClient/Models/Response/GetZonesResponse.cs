namespace DnsApiClient.Models.Response;

public class GetZonesResponse
{
    public List<ZoneModel> Zones { get; set; } = new();
    public MetaData Meta { get; set; } = new();
    
    public class MetaData
    {
        public PaginationData Pagination { get; set; } = new();
        
        public class PaginationData
        {
            public long Page { get; set; }
            public long PerPage { get; set; }
            public long PreviousPage { get; set; }
            public long NextPage { get; set; }
            public long LastPage { get; set; }
            public long TotalEntries { get; set; }
        }
    }
}