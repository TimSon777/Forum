using Domain.Data;
using Domain.Events;

namespace Forum.Consumer;

public static class Mapping
{
    public static SaveMessageItem Map(this MessageEvent message) => new()
    {
        Text = message.Text,
        FileId = message.FileId,
        UserName = message.UserName
    };
}