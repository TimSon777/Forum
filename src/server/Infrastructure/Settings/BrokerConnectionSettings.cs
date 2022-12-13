// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public class BrokerConnectionSettings : ISettings
{
    public static string SectionName => "BROKER_CONNECTION_SETTINGS";

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