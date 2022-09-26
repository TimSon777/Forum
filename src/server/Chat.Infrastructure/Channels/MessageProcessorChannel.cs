using RabbitMQ.Client;

namespace Chat.Infrastructure.Channels;

public class MessageProcessorChannel : IMessageProcessorChannel
{
    public IModel Channel { get; }

    public MessageProcessorChannel(IConnection connection)
    {
        Channel = connection.CreateModel();
    }
}