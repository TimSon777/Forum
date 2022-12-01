using Forum.Contracts;
using Forum.Queue.Listener.Abstractions;
using MassTransit;

namespace Forum.Queue.Listener;

public class MessageSaverConsumer : IConsumer<MessageContract>
{
    private readonly IForumRepository _forumRepository;

    public MessageSaverConsumer(IForumRepository forumRepository)
    {
        _forumRepository = forumRepository;
    }

    public async Task Consume(ConsumeContext<MessageContract> context)
    {
        var message = context.Message;
        await _forumRepository.SaveMessageAsync(message.UserName, message.Text, message.FileKey);
    }
}