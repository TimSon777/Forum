using Application.Abstractions;
using Domain.Events;
using MassTransit;

namespace Forum.Consumer;

public class MessageSaverConsumer : IConsumer<MessageEvent>
{
    private readonly IMessageRepository _messageRepository;

    public MessageSaverConsumer(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task Consume(ConsumeContext<MessageEvent> context)
    {
        var message = context.Message.Map();
        await _messageRepository.SaveMessageAsync(message);
    }
}