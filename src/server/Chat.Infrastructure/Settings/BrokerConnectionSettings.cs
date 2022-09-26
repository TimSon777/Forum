using Microsoft.Extensions.Configuration;

namespace Chat.Infrastructure.Settings;

// ReSharper disable once ClassNeverInstantiated.Global
public class BrokerConnectionSettings
{
    public const string Position = "BROKER_CONNECTION_SETTINGS";
    
    [ConfigurationKeyName("HOST_NAME")] 
    public string HostName { get; set; } = "";
}