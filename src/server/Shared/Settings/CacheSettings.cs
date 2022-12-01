using Microsoft.Extensions.Configuration;

namespace Shared.Settings;

public sealed class CacheSettings
{
    public const string Position = "CACHE_SETTINGS";

    [ConfigurationKeyName("PASSWORD")]
    public string Password { get; set; } = default!;

    [ConfigurationKeyName("HOST")]
    public string Host { get; set; } = default!;

    [ConfigurationKeyName("PORT")]
    public int Port { get; set; }
}