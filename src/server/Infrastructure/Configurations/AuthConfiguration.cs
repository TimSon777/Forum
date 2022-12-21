using Infrastructure.Abstractions;
using Infrastructure.Implementations;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class AuthConfiguration
{
    public static IServiceCollection AddJwtProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.Configure<AuthSettings>(configuration.GetSection(AuthSettings.SectionName));
        return services;
    }
}