using Cache;
using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Shared.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class CacheConfiguration
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEasyCaching(options =>
        {
            options.UseRedis(configurator =>
                {
                    var settings = configuration
                        .Get<CacheSettings>(CacheSettings.Position);

                    var redisEndpoint = settings.Map();

                    configurator.DBConfig.Password = settings.Password;
                    configurator.DBConfig.Endpoints.Add(redisEndpoint);
                })
                .WithMessagePack();
        });

        services.AddSingleton<IRedisCachingProvider>(sp =>
            sp.GetRequiredService<IEasyCachingProviderFactory>().GetRedisProvider("DefaultRedis"));

        return services;
    } 
}