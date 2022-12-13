using Metadata.API;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class BackgroundServicesConfiguration
{
    public static IServiceCollection AddCollectionCreatorBackgroundService(this IServiceCollection services)
    {
        services.AddHostedService<CollectionCreatorBackgroundService>();
        return services;
    }
}