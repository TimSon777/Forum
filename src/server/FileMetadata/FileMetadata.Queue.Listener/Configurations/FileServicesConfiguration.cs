using FileMetadata.Queue.Listener.Abstractions;
using FileMetadata.Queue.Listener.Implementations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class FileServicesConfiguration
{
    public static IServiceCollection AddFileServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileMover, FileMover>();
        return services;
    }
}