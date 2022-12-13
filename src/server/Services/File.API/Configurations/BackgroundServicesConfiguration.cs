using File.API;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class BackgroundServicesConfiguration
{
    public static IServiceCollection AddBucketCreatorBackgroundService(this IServiceCollection services)
    {
        services.AddHostedService<BucketCreatorBackgroundService>();
        return services;
    }
}