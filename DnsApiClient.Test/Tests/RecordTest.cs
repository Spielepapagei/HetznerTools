using DnsApiClient.Extensions;
using DnsApiClient.Http;
using DnsApiClient.Models;
using DnsApiClient.Models.Request;
using DnsApiClient.Test.Extensions;
using Spectre.Console;

namespace DnsApiClient.Test.Tests;

public class RecordTest
{
    private readonly RecordsClient Client;

    public RecordTest(RecordsClient client)
    {
        Client = client;
    }
    
    public async Task Test(string zoneId)
    {
        await CreateGetUpdateDeleteRecord(new CreateRecordRequest
        {
            ZoneId = zoneId,
            Name = "Testy",
            Type = RecordType.A,
            Value = "0.0.0.0"
        });
        
        AnsiConsole.WriteLine("Successfully finished Zone Test.");
    }

    private async Task CreateGetUpdateDeleteRecord(CreateRecordRequest data)
    {
        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots).SpinnerStyle(Style.Parse("green"))
            .StartAsync("init...", async ctx  =>
            {
                //Creating Record
                ctx.Status("Creating record");
                var x1 = await Client.CreateRecord(data);
                var newRecord = x1.TryGetData("Failed to create record");

                //Updating Record
                ctx.Status("Updating record");
                data.Value = "172.0.0.0"; //Alternate record data
                var x2 = await Client.UpdateRecord(newRecord.Record.Id, data);
                var updatedRecord = x2.TryGetData("Failed to update record");
                
                //Get Record
                ctx.Status("Getting record");
                var x3 = await Client.GetRecord(newRecord.Record.Id);
                var record = x3.TryGetData("Failed to get record");
                
                //Get all Records
                ctx.Status("Getting all records");
                var x4 = await Client.GetAllRecords(data.ZoneId);
                var records = x4.TryGetData("Failed to get records");
                
                //Delete Record
                ctx.Status("Deleting Records");
                var x0 = await Client.DeleteRecord(newRecord.Record.Id);
                x0.TryGetData("Failed to delete record");
            });
    }
    
}