using Chat.Infrastructure.BackgroundServices;
using Chat.Infrastructure.Channels;
using Chat.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class HostedServicesConfiguration
{
    public static IServiceCollection AddMessageConsumer(this IServiceCollection services, 
        IConfiguration configuration,
        ILogger logger,
        int retry)
    {
        var settings = configuration.Get<BrokerConnectionSettings>(BrokerConnectionSettings.Position);
        var connectionFactory = new ConnectionFactory
        {
            HostName = settings.HostName,
            DispatchConsumersAsync = true
        };

        services.Configure<ConsumerSettings>(configuration, ConsumerSettings.Position);

        IConnection connection = null!;

        for (var i = 0; i < retry; i++)
        {
            logger.LogInformation("Try connect to broker. Attempt {attempt}", i);
            try
            {
                connection = connectionFactory.CreateConnection();
                break;
            }
            catch (BrokerUnreachableException)
            {
                if (i == retry - 1)
                {
                    throw;
                }
                
                logger.LogWarning("Connect to broker was failed");
            }
        }

        var chanel = new MessageProcessorChannel(connection);
        services.AddSingleton<IMessageProcessorChannel>(chanel);

        services.AddHostedService<MessageConsumer>();

        return services;
    }
}