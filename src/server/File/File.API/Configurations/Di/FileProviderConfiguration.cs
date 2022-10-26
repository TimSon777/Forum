using File.API.Abstractions;
using File.API.Implementations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class FileProviderConfiguration
{
    public static IServiceCollection AddFileProvider(this IServiceCollection services)
    {
        services.AddScoped<IFileProvider, FileProvider>();
        return services;
    }
}