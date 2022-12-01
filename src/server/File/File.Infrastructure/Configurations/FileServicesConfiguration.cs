using File.Application.Abstractions;
using File.Infrastructure.Implementations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class FileServicesConfiguration
{
    public static IServiceCollection AddFileProvider(this IServiceCollection services)
    {
        services.AddSingleton<IFileProvider, FileProvider>();
        return services;
    }

    public static IServiceCollection AddBucketCreator(this IServiceCollection services)
    {
        services.AddSingleton<IBucketCreator, BucketCreator>();
        return services;
    }
}