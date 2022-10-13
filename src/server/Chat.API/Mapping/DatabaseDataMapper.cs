using Chat.API.Data;
using Chat.DAL.Abstractions.Chat.Data;

namespace Chat.API.Mapping;

public static class DatabaseDataMapper
{
    public static SendMessageItem ToSendMessageItem(this GetMessageItemStorage item)
    {
        return new SendMessageItem
        {
            Name = item.User.Name,
            Text = item.Text
        };
    }
}