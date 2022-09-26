using Chat.Infrastructure.Chanels;
using RabbitMQ.Client;

namespace Chat.Infrastructure.Channels;

public class MessageProcessorChanel : IMessageProcessorChanel
{
    public IModel Channel { get; }

    public MessageProcessorChanel(IConnection connection)
    {
        Channel = connection.CreateModel();
    }
}