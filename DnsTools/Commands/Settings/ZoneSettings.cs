using System.ComponentModel;
using Spectre.Console.Cli;

namespace DnsTools.Commands.Settings;

public class ZoneSettings : CommandSettings
{
    public class Zones : ZoneSettings
    {
        [Description("The name of a zone you want info from leave blank for all zones")]
        [CommandOption("-z|--zone <ZONE>")]
        public string? ZoneName { get; set; }
    }
    
    public class CreateZone : ZoneSettings
    {
        [CommandArgument(0, "<ZONE_NAME>")]
        public string ZoneName { get; set; }
        
        [CommandOption("-t|--ttl <TTL>")]
        public int Ttl { get; set; }
    }
}