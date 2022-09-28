using Microsoft.Extensions.Configuration;

namespace Chat.Shared.Settings;

// ReSharper disable once ClassNeverInstantiated.Global
public class PostgresSettings
{
    public const string Position = "POSTGRESQL_SETTINGS";

    [ConfigurationKeyName("HOST")]
    public string Host { get; set; } = "";

    [ConfigurationKeyName("PASSWORD")]
    public string Password { get; set; } = "";

    [ConfigurationKeyName("USER_NAME")]
    public string UserName { get; set; } = "";

    [ConfigurationKeyName("DATABASE_NAME")]
    public string DatabaseName { get; set; } = "";

    public override string ToString()
    {
        return $"Host={Host}; user id={UserName}; password={Password}; database={DatabaseName}";
    }
}