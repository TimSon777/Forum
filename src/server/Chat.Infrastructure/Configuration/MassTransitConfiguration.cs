using Chat.Infrastructure.MessageHandlers;
using MassTransit;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMessageHandlers(this IServiceCollection services)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<MessageSaverConsumer>();
            configurator.UsingInMemory((ctx, inMemoryConfigurator) =>
            {
                inMemoryConfigurator.ReceiveEndpoint(new TemporaryEndpointDefinition(),
                    endpointConfigurator =>
                    {
                        endpointConfigurator.ConfigureConsumer<MessageSaverConsumer>(ctx);
                    });
            });
        });
        
        return services;
    }
}