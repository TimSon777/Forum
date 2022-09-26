using RabbitMQ.Client;

namespace Chat.Infrastructure.Channels;

public interface IMessageProcessorChannel
{
    IModel Channel { get; }
}