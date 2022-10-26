using Microsoft.Extensions.Configuration;

namespace Shared.Settings;

// ReSharper disable once ClassNeverInstantiated.Global
public class BrokerConnectionSettings
{
    public const string Position = "BROKER_CONNECTION_SETTINGS";
    
    [ConfigurationKeyName("HOST")] 
    public string Host { get; set; } = "";

    [ConfigurationKeyName("PASSWORD")] 
    public string Password { get; set; } = "";
    
    [ConfigurationKeyName("USER_NAME")] 
    public string UserName { get; set; } = "";

    public override string ToString()
    {
        return $"Host name: {Host}, User name: {UserName} Password: {Password}";
    }
}