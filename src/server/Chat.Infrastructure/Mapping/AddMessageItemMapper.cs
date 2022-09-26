using Chat.Core.Data;
using Chat.DAL.Abstractions;
using Chat.DAL.Abstractions.Chat;

namespace Chat.Infrastructure.Mapping;

public static class AddMessageItemMapper
{
    public static AddMessageItem ToAddMessageItem(this Message message)
    {
        Guard.Against.Null(message);
        
        var user = new AddUserItem
        {
            Name = message.User.Name
        };
        
        return new AddMessageItem
        {
            Text = message.Text,
            User = user
        };
    }
}