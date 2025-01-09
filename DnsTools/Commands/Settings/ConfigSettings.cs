using System.ComponentModel;
using Spectre.Console.Cli;

namespace DnsTools.Commands.Settings;

public class ConfigSettings : CommandSettings
{

    public class TokenSettings : ConfigSettings
    {
        public class GetToken : TokenSettings
        {
            
        }
        
        public class SetToken : TokenSettings
        {
        
        }
    }
    
}