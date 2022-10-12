using Chat.Infrastructure.MessageHandlers;
using Chat.Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMessageHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        var brokerConnectionSettings = configuration.Get<BrokerConnectionSettings>(BrokerConnectionSettings.Position);
        
        services.AddMassTransit(configurator =>
        {
            configurator.AddLogging();
            configurator.AddConsumer<MessageSaverConsumer>();
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
                        endpointConfigurator.Durable = true;
                        endpointConfigurator.AutoDelete = false;
                        endpointConfigurator.ExchangeType = "fanout";
                        endpointConfigurator.ConfigureConsumer<MessageSaverConsumer>(ctx);
                    });
            });
        });
        
        return services;
    }
}