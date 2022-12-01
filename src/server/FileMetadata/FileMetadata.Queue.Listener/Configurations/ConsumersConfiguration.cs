using FileMetadata.Queue.Listener;
using MassTransit;
using Shared.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ConsumersConfiguration
{
    public static IServiceCollection AddFileAndMetadataSaverConsumer(this IServiceCollection services, IConfiguration configuration)
    {
        var brokerConnectionSettings = configuration.Get<BrokerConnectionSettings>(BrokerConnectionSettings.Position);
        
        services.AddMassTransit(configurator =>
        {
            configurator.AddLogging();
            configurator.AddConsumer<FileAndMetadataSaverConsumer>();
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
                        endpointConfigurator.Durable = true;
                        endpointConfigurator.AutoDelete = false;
                        endpointConfigurator.ExchangeType = "fanout";
                        endpointConfigurator.ConfigureConsumer<FileAndMetadataSaverConsumer>(ctx);
                    });
                
                brokerConfigurator.ConfigureEndpoints(ctx);
            });
        });
        
        return services;
    }
}