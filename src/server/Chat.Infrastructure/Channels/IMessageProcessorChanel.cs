using RabbitMQ.Client;

namespace Chat.Infrastructure.Chanels;

public interface IMessageProcessorChanel
{
    IModel Channel { get; }
}