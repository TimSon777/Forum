using Domain.Entities;

namespace Forum.API.Features.Messages.History;

public static class Mapping
{
    public static Response Map(this Message message) => new()
    {
        Text = message.Text,
        FileKey = message.FileKey,
        UserName = message.User.Name
    };
}