using DnsTools.Configuration;
using Spectre.Console;

namespace DnsTools.Services;

public class EnvironmentConfigService
{
    private EnvironmentConfig Data = new();

    public EnvironmentConfigService()
    {
        Reload();
    }

    public EnvironmentConfig Get()
    {
        return Data;
    }
    
    public void Reload()
    {
        foreach (var prop in typeof(EnvironmentConfig).GetProperties())
        {
            var envVar = Environment.GetEnvironmentVariable(prop.Name, EnvironmentVariableTarget.User);
            prop.SetValue(Data, envVar);
        }
        
        Save(Data);
    }

    public void Save(EnvironmentConfig data)
    {
        foreach (var property in typeof(EnvironmentConfig).GetProperties())
        {
            var value = property.GetValue(data, null);
            if(value == null) return;
            if (value.GetType() != typeof(string))
            {
                throw new NotSupportedException();
            }
            Environment.SetEnvironmentVariable(property.Name, value.ToString(), EnvironmentVariableTarget.User);
        }
    }
}