using Chat.Infrastructure.MessageHandlers.Data;

namespace Chat.Infrastructure.Mapping;

public static class ConsumerDataMapper
{
    public static GetMessageConsumerItem ToGetMessageConsumerItem(this GetMessageHubItem hubItem)
    {
        var user = new GetUserConsumerItem
        {
            Name = string.Join(":", hubItem.IPv4, hubItem.Port)
        };

        return new GetMessageConsumerItem
        {
            User = user,
            Text = hubItem.Text
        };
    }
}