using System.Runtime.InteropServices;
using DnsApiClient.Http;
using DnsApiClient.Models.Request;
using DnsApiClient.Test.Helper;
using Spectre.Console;

namespace DnsApiClient.Test.Tests;

public class ZoneTest
{
    private readonly ZonesClient Client;

    public ZoneTest(ZonesClient client)
    {
        Client = client;
    }
    
    public async Task Test()
    {
        await CreateGetUpdateDeleteZone("DnsApiClient.dev", 86400);
        
        
        AnsiConsole.WriteLine("Successfully finished Zone Test.");
    }

    private async Task CreateGetUpdateDeleteZone(string zoneName, int ttl)
    {
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots).SpinnerStyle(Style.Parse("green"))
            .StartAsync("Creating Zone...", async ctx  =>
            {
                var data = new CreateZoneRequest
                {
                    Name = zoneName.ToLower(),
                    Ttl = ttl
                };

                
                //Creating Zone
                var x1 = await Client.CreateZone(data);
                if (x1.Data == null)
                {
                    AnsiConsole.MarkupLine($"Creating Zone: {x1.Message}");
                    throw new Exception();
                }
                AnsiConsole.MarkupLine(x1.Message);
                
                //Getting zoneId for next Step
                var zoneId = x1.Data.Id;
                if (zoneId == null)
                {
                    AnsiConsole.MarkupLine($"Failed to get zoneId: {zoneName}");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                
                //Getting All Zone Data
                ctx.Status("Getting all zone Data...");
                var x2 = await Client.GetZones();
                if (x2.Data == null)
                {
                    AnsiConsole.MarkupLine($"Failed all zone data: {x2.Message}");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                AnsiConsole.MarkupLine(x2.Message);
                
                
                //Getting Zone Data
                ctx.Status("Getting zone data...");
                var x3 = await Client.GetZone(zoneId);
                if (x3.Data == null)
                {
                    AnsiConsole.MarkupLine($"Failed getting zone data: {x3.Message}");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                AnsiConsole.MarkupLine(x3.Message);
                
                
                //Updating Zone
                ctx.Status("Updating zone...");
                data.Ttl -= 4000;
                var x4 = await Client.UpdateZone(zoneId, data);
                if (x4.Data == null)
                {
                    AnsiConsole.MarkupLine($"Failed updating zone: {x4.Message}");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                AnsiConsole.MarkupLine(x4.Message);
                
                
                // Load zoneFile
                ctx.Status("Loading ZoneFile...");
                var fileExists = File.Exists(PathBuilder.File("storage", "zone"));
                if (!fileExists)
                {
                    AnsiConsole.MarkupLine("Please Create a file at ./storage/zone");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new FileNotFoundException();
                }
                AnsiConsole.MarkupLine("Successfully Loaded zoneFile from disk.");

                var zoneFile = await File.ReadAllTextAsync(PathBuilder.File("storage", "zone"));
                AnsiConsole.Write(new Panel($"[gray]{zoneFile}[/]")
                    .Header("ZoneFile")
                    .Expand()
                    .Border(BoxBorder.None));


                /*
                ctx.Status("Validating ZoneFile...");
                data.Name += "v";
                var x5 = await Client.ValidateZoneFile(zoneFile);
                if (x5.Data == null)
                {
                    AnsiConsole.MarkupLine($"Failed validating ZoneFile: {x5.Message}");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                AnsiConsole.MarkupLine(x5.Message);


                ctx.Status("Importing ZoneFile...");
                data.Name += "v";
                var x6 = await Client.ImportZoneFile(zoneId, zoneFile);
                if (x6.Data == null)
                {
                    AnsiConsole.MarkupLine($"Failed importing ZoneFile: {x6.Message}");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                AnsiConsole.MarkupLine(x6.Message);
                */
    
                ctx.Status("Exporting ZoneFile...");
                data.Name += "v";
                var x7 = await Client.ExportZoneFile(zoneId);
                if (x7.Data == null)
                {
                    AnsiConsole.MarkupLine($"Failed exporting ZoneFile: {x7.Message}");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                
                /*
                if (x7.Data != zoneFile.Replace("\r", ""))
                {
                    AnsiConsole.MarkupLine("Error got wrong data back from Server!");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                */
                AnsiConsole.MarkupLine(x7.Message);
                
                
                ctx.Status("Deleting Zone...");
                if(x1.Data != null) await DeleteZone(x1.Data.Id);
            });
    }

    private async Task DeleteZone(string zoneId)
    {
        var x0 = await Client.DeleteZone(zoneId);
        if (x0.Data == null)
        {
            AnsiConsole.MarkupLine(x0.Message);
            throw new Exception();
        }
        AnsiConsole.MarkupLine(x0.Message);
    }



}