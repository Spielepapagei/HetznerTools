namespace DnsTools.Http.Models.Response;

public class GetZonesResponse
{
    public MetaData Meta { get; set; }
    public List<ZoneModel> Zones { get; set; } = new();
    
    public class MetaData
    {
        public PaginationData Pagination { get; set; }
        
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