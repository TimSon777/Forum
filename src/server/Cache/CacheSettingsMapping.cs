using EasyCaching.Core.Configurations;
using Shared.Settings;

namespace Cache;

public static class CacheSettingsMapping
{
    public static ServerEndPoint Map(this CacheSettings settings) => new()
    {
        Host = settings.Host,
        Port = settings.Port
    };
}