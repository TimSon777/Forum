// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        return services;
    }
}