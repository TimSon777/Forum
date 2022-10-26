using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    public static IServiceCollection Configure<T>(this IServiceCollection services, 
        IConfiguration configuration,
        string position) 
        where T : class, new()
    {
        var section = configuration.GetSection(position);
        services.Configure<T>(section);
        return services;
    }
}