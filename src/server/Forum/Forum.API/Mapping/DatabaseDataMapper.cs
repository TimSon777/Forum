using Forum.API.Data;
using Forum.DAL.Abstractions.Chat.Data;

namespace Forum.API.Mapping;

public static class DatabaseDataMapper
{
    public static SendMessageItem ToSendMessageItem(this GetMessageItemStorage item)
    {
        return new SendMessageItem
        {
            Name = item.User.Name,
            Text = item.Text,
            FileKey = item.FileKey
        };
    }
}