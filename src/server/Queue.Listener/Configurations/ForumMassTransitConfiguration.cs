using MassTransit;
using Queue.Listener.Forum;
using Queue.Listener.Forum.Consumer;
using Shared.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ForumMassTransitConfiguration
{
    public static IServiceCollection AddMessageSaverConsumer(this IServiceCollection services, IConfiguration configuration)
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
                        var exchangeName = configuration.GetString("MESSAGE_EXCHANGE_NAME");
                        endpointConfigurator.Bind(exchangeName);
                        endpointConfigurator.Durable = true;
                        endpointConfigurator.AutoDelete = false;
                        endpointConfigurator.ExchangeType = "fanout";
                        endpointConfigurator.ConfigureConsumer<MessageSaverConsumer>(ctx);
                    });
                
                brokerConfigurator.ConfigureEndpoints(ctx);
            });
        });
        
        return services;
    }
}