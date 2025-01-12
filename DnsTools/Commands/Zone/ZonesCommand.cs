using DnsTools.Commands.Settings;
using DnsTools.Http;
using DnsTools.Http.Models.Response;
using DnsTools.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using ThwCalendarExporter.Helper;

namespace DnsTools.Commands.Zone;

public class ZonesCommand : AsyncCommand<ZoneSettings.Zones>
{
    private readonly ZoneHttpClient Client;
    private readonly EnvironmentConfigService EnvConfigService;

    public ZonesCommand(ZoneHttpClient client, EnvironmentConfigService envConfigService)
    {
        Client = client;
        EnvConfigService = envConfigService;
    }
    
    public override async Task<int> ExecuteAsync(CommandContext context, ZoneSettings.Zones settings)
    {
        Validate(context, settings);
        var response = await Client.GetZone(settings.ZoneName);

        if (response.Data != null)
        {
            ZoneModel? selectedZone = null;
            // Sets the currently selected Zone.
            if (response.Data.Zones is { Count: > 1 })
                selectedZone = SelectZone(response.Data.Zones);
            else if (response.Data.Zones != null)
                selectedZone = response.Data.Zones.First();
            else if (response.Data.Zone != null) 
                selectedZone = response.Data.Zone;

            if (selectedZone == null)
            {
                AnsiConsole.MarkupLine($"{LogPrefixes.Error} Something unexpected happened.");
                return 0;
            }
            
            //Select an Action for the zone.
            var action = SelectAction();
            switch (action)
            {
                case "Set as Active Zone":
                    EnvConfigService.Get().ActiveZone = selectedZone.Id;
                    EnvConfigService.Save(EnvConfigService.Get());
                    break;
                case "Import Zone File": //Validate with POST /zones/file/validate => POST /zones/file/validate
                    
                    break;
                case "Export Zone File": //GET /zones/{ZoneID}/export
                    
                    break;
                case "Edit Zone File":
                    
                    break;
                case "Delete Zone":
                    
                    break;
                case "Go Back":
                    SelectZone(response.Data.Zones);
                    break;
                
                default:
                    AnsiConsole.MarkupLine($"{LogPrefixes.Warning} {action} is not Implemented.");
                    SelectAction();
                    break;
            }
            
            //TODO: Implement logic!
            AnsiConsole.MarkupLine("Selected Zone: " + selectedZone.Name + $"and Executing {action} Action");
        }
        else
        {
            AnsiConsole.MarkupLine(response.Message);
        }
        
        return 0;
    }

    private ZoneModel SelectZone(List<ZoneModel> zones)
    {
        var selectionPrompt = new SelectionPrompt<ZoneModel>
        {
            Title = "Select a Zone",
            MoreChoicesText = "[grey](Move up and down to view more)[/]",
            PageSize = 10,
            SearchEnabled = true,
            SearchHighlightStyle = new Style(Color.Red),
            HighlightStyle = new Style(Color.Blue),
            Converter = zone => $"{zone.Name} - '{zone.Id}'"
        };
        
        foreach (var zone in zones)
        {
            selectionPrompt.AddChoice(zone);
        }

        var selectedZone = AnsiConsole.Prompt(selectionPrompt);
        return selectedZone;
    }
    
    private string SelectAction()
    {
        var selectionPrompt = new SelectionPrompt<string>
        {
            Title = "Select a Action",
            PageSize = 10,
            HighlightStyle = new Style(Color.Blue),
            Converter = action => $"{action.ToString()}"
        };

        selectionPrompt.AddChoices(
            "Set as Active Zone",
            "Import Zone File", //Validate with POST /zones/file/validate => POST /zones/file/validate
            "Export Zone File", //GET /zones/{ZoneID}/export
            "Edit Zone File",
            "Delete Zone",
            "Go Back"
        );
        
        selectionPrompt.AddChoices();

        return AnsiConsole.Prompt(selectionPrompt);
    }
}