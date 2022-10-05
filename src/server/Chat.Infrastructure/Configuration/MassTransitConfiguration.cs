using Chat.Infrastructure.MessageHandlers;
using MassTransit;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMessageHandlers(this IServiceCollection services)
    {
        var consumer = typeof(MessageSaverConsumer);
        
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer(consumer);
            configurator.UsingInMemory();
        });

        return services;
    }
}