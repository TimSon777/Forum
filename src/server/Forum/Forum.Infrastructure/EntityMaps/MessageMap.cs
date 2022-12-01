using Dapper.FluentMap.Mapping;
using Forum.Domain.Entities;

namespace Forum.Infrastructure.EntityMaps;

public class MessageMap : EntityMap<Message>
{
    public MessageMap()
    {
        Map(x => x.Text).ToColumn("MessageText", false);
        Map(x => x.FileKey).ToColumn("FileKey", false);
    }
}