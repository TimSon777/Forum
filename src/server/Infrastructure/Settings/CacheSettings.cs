// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public sealed class CacheSettings : ISettings
{
    public static string SectionName => "CACHE_SETTINGS";

    [ConfigurationKeyName("PASSWORD")]
    public string Password { get; set; } = default!;

    [ConfigurationKeyName("HOST")]
    public string Host { get; set; } = default!;

    [ConfigurationKeyName("PORT")]
    public int Port { get; set; }
}