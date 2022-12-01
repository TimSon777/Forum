using Forum.Contracts;
using Forum.Infrastructure.MessageHandlers.Data;

namespace Forum.Infrastructure.Mapping;

public static class HubMapping
{
    public static MessageContract Map(this GetMessageHubItem item) => new()
    {
        Text = item.Text,
        FileKey = item.FileKey,
        UserName = item.IpAddress.ToString()
    };
    
    public static SendMessageHubItem Map(this MessageContract item) => new()
    {
        Text = item.Text,
        FileKey = item.FileKey,
        UserName = item.UserName
    };
}