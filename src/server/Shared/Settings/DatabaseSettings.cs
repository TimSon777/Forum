using Microsoft.Extensions.Configuration;

namespace Shared.Settings;

// ReSharper disable once ClassNeverInstantiated.Global
public class DatabaseSettings
{
    public const string Position = "DATABASE_SETTINGS";

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

    public string ToString(DatabaseType databaseType)
    {
        return $@"mongodb://{UserName}:{Password}@{Host}";
    }
    
    public static implicit operator string(DatabaseSettings settings)
    {
        return settings.ToString();
    } 
}

public enum DatabaseType
{
    Mongo,
}