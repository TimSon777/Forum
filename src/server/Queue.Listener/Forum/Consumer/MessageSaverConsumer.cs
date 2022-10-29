using Broker.Contracts.Forum;
using MassTransit;
using Queue.Listener.Forum.Database.Repositories.Abstractions;

namespace Queue.Listener.Forum.Consumer;

// ReSharper disable once UnusedType.Global
// ReSharper disable once ClassNeverInstantiated.Global
public class MessageSaverConsumer : IConsumer<GetMessageConsumerItem>
{
    private readonly IForumRepository _forumRepository;

    public MessageSaverConsumer(IForumRepository forumRepository)
    {
        _forumRepository = forumRepository;
    }

    public async Task Consume(ConsumeContext<GetMessageConsumerItem> context)
    {
        var message = context.Message;
        await _forumRepository.SaveMessageAsync(message.User.Name, message.Text, message.FileKey);
    }
}