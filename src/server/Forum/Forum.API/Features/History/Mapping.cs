using Forum.API.Features.History.Responses;
using Forum.Domain.Entities;

namespace Forum.API.Features.History;

public static class Mapping
{
    public static GetMessageResponse Map(this Message message) => new()
    {
        Text = message.Text,
        FileKey = message.FileKey,
        UserName = message.User.Name
    };
}