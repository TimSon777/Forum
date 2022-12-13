using Domain.Events;
using Forum.Handler.Data;

namespace Forum.API;

public static class Mapping
{
    public static MessageEvent Map(this GetMessageHubItem item) => new()
    {
        Text = item.Text,
        FileId = item.FileKey,
        UserName = item.IpAddress.ToString()
    };
    
    public static SendMessageHubItem Map(this MessageEvent item) => new()
    {
        Text = item.Text,
        FileKey = item.FileId,
        UserName = item.UserName
    };
}