using Chat.Core.Data;
using Chat.Infrastructure.Data;

namespace Chat.Infrastructure.Mapping;

public static class GetMessageItemMapper
{
    public static Message ToMessage(this GetMessageItem src)
    {
        Guard.Against.Null(src);
        var user = new User(src.IPv4, src.Port);
        return new Message(user, src.Text);
    }
}