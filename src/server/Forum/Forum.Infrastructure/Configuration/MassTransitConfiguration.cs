using MassTransit;
using Microsoft.Extensions.Configuration;
using Shared.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMassTransitPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        var brokerConnectionSettings = configuration.Get<BrokerConnectionSettings>(BrokerConnectionSettings.Position);
        
        services.AddMassTransit(configurator =>
        {
            configurator.AddLogging();
            configurator.UsingRabbitMq((ctx, brokerConfigurator) =>
            {
                brokerConfigurator.Host(brokerConnectionSettings.Host, "/", hostConfigurator =>
                {
                    hostConfigurator.Password(brokerConnectionSettings.Password);
                    hostConfigurator.Username(brokerConnectionSettings.UserName);
                });
                
                brokerConfigurator.ReceiveEndpoint(new TemporaryEndpointDefinition(),
                    endpointConfigurator =>
                    {
                        var exchangeName = configuration.GetString("EXCHANGE_NAME");
                        endpointConfigurator.Bind(exchangeName);
                    });
                
                brokerConfigurator.ConfigureEndpoints(ctx);
            });
        });
        
        return services;
    }
}