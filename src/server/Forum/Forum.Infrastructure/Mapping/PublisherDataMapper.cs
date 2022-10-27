using Broker.Contracts.Forum;
using Forum.Infrastructure.MessageHandlers.Data;

namespace Forum.Infrastructure.Mapping;

public static class PublisherDataMapper
{
    public static GetMessageConsumerItem ToGetMessageConsumerItem(this GetMessageHubItem hubItem)
    {
        var user = new GetUserConsumerItem
        {
            Name = hubItem.IPv4.ToString()
        };

        return new GetMessageConsumerItem
        {
            User = user,
            Text = hubItem.Text,
            FileKey = hubItem.FileKey
        };
    }

    public static SendMessageHubItem ToSendMessageHubItem(this GetMessageConsumerItem publisherItem)
    {
        return new SendMessageHubItem
        {
            Name = publisherItem.User.Name,
            Text = publisherItem.Text,
            FileKey = publisherItem.FileKey 
        };
    }
}