using Domain.Events;
using Forum.API.Data;

namespace Forum.API;

public static class Mapping
{
    public static MessageEvent Map(this GetMessageHubItem item, string userName) => new()
    {
        Text = item.Text,
        FileId = item.FileKey,
        UserName = userName
    };
    
    public static SendMessageHubItem Map(this MessageEvent item) => new()
    {
        Text = item.Text,
        FileKey = item.FileId,
        UserName = item.UserName
    };
}