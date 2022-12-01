using Metadata.Application.Abstractions;
using Metadata.Infrastructure.Implementations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MetadataProviderConfiguration
{
    public static IServiceCollection AddMetadataProvider(this IServiceCollection services)
    {
        services.AddSingleton<IMetadataProvider, MetadataProvider>();
        return services;
    }
}