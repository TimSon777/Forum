using Chat.Infrastructure.MessageHandlers.Data;

namespace Chat.Infrastructure.Mapping;

public static class ConsumerDataMapper
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
            Text = hubItem.Text
        };
    }

    public static SendMessageHubItem ToSendMessageHubItem(this GetMessageConsumerItem consumerItem)
    {
        return new SendMessageHubItem
        {
            Name = consumerItem.User.Name,
            Text = consumerItem.Text
        };
    }
}