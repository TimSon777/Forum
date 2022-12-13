using Application.Abstractions;
using Domain.Events;
using MassTransit;

namespace Forum.Consumer;

public class MessageSaverConsumer : IConsumer<MessageEvent>
{
    private readonly IForumRepository _forumRepository;

    public MessageSaverConsumer(IForumRepository forumRepository)
    {
        _forumRepository = forumRepository;
    }

    public async Task Consume(ConsumeContext<MessageEvent> context)
    {
        var message = context.Message.Map();
        await _forumRepository.SaveMessageAsync(message);
    }
}