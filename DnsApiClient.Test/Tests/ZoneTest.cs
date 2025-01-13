using System.Runtime.InteropServices;
using DnsApiClient.Http;
using DnsApiClient.Models.Request;
using DnsApiClient.Test.Helper;
using Spectre.Console;

namespace DnsApiClient.Test.Tests;

public class ZoneTest
{
    private readonly ZonesClient Client;
    private readonly RecordTest RecordTest;
    private string ZoneId = "";

    public ZoneTest(ZonesClient client, RecordTest recordTest)
    {
        Client = client;
        RecordTest = recordTest;
    }

    public async Task Test()
    {
        try
        {
            await CreateGetUpdateDeleteZone(new CreateZoneRequest
            {
                Name = "DnsApiClient.dev",
                Ttl = 86400
            });

            //Start RecordTest
            await RecordTest.Test(ZoneId);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenMethods);
        }
        
        //Delete Zone
        var zone = await Client.DeleteZone(ZoneId);
        zone.TryGetData("Failed to delete Zone");
        
        //Finished Tests
        AnsiConsole.WriteLine("Successfully finished Zone Test.");
    }

    private async Task CreateGetUpdateDeleteZone(CreateZoneRequest data)
    {
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots).SpinnerStyle(Style.Parse("green"))
            .StartAsync("init...", async ctx =>
            {
                //Create Zone
                ctx.Status("Creating Zone");
                var x1 = await Client.CreateZone(data);
                var createdZone = x1.TryGetData("Failed to create zone");
                ZoneId = createdZone.Id;

                //Get all zones
                ctx.Status("Getting all zones");
                var x2 = await Client.GetZones();
                x2.TryGetData("Failed to get all zones");


                //Get Zone
                ctx.Status("Getting Zone");
                var x3 = await Client.GetZone(createdZone.Id);
                x3.TryGetData("Failed to get zone");


                //Update Zone
                ctx.Status("Updating zone...");
                data.Ttl -= 4000;
                var x4 = await Client.UpdateZone(createdZone.Id, data);
                x4.TryGetData("Failed updating zone");


                // Load zoneFile
                ctx.Status("Loading ZoneFile...");
                var fileExists = File.Exists(PathBuilder.File("storage", "zone"));
                if (!fileExists)
                {
                    AnsiConsole.MarkupLine("Please Create a file at ./storage/zone");
                    if (x1.Data != null) await DeleteZone(createdZone.Id);
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
                var x7 = await Client.ExportZoneFile(createdZone.Id);
                x7.TryGetData("Failed to export ZoneFile");

                /*
                if (x7.Data != zoneFile.Replace("\r", ""))
                {
                    AnsiConsole.MarkupLine("Error got wrong data back from Server!");
                    if(x1.Data != null) await DeleteZone(x1.Data.Id);
                    throw new Exception();
                }
                */
            });
    }
    
    private async Task DeleteZone(string zoneId)
    {
        var zone = await Client.DeleteZone(zoneId);
        zone.TryGetData("Failed to delete Zone");
    }
}