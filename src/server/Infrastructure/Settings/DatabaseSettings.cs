// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public class DatabaseSettings : ISettings
{
    public static string SectionName => "DATABASE_SETTINGS";

    [ConfigurationKeyName("HOST")]
    public string Host { get; set; } = "";

    [ConfigurationKeyName("PASSWORD")]
    public string Password { get; set; } = "";

    [ConfigurationKeyName("USER_NAME")]
    public string UserName { get; set; } = "";

    [ConfigurationKeyName("DATABASE_NAME")]
    public string DatabaseName { get; set; } = "";

    [ConfigurationKeyName("PORT")]
    public int Port { get; set; }

    public string ToString(DatabaseType databaseType)
    {
        return databaseType switch
        {
            DatabaseType.Mongo => $@"mongodb://{UserName}:{Password}@{Host}:{Port}",
            DatabaseType.Postgres => $"Host={Host}; user id={UserName}; password={Password}; database={DatabaseName}",
            _ => throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null)
        };
    }
}

public enum DatabaseType
{
    Mongo,
    Postgres
}