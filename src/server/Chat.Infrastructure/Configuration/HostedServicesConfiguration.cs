using Chat.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class HostedServicesConfiguration
{
    public static IServiceCollection ConfigureBrokerSettings(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<BrokerConnectionSettings>(configuration, BrokerConnectionSettings.Position);
        services.Configure<ConsumerSettings>(configuration, ConsumerSettings.Position);
        return services;
    }
}