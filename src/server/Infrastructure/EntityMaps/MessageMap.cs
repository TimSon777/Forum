using Dapper.FluentMap.Mapping;
using Domain.Entities;

namespace Infrastructure.EntityMaps;

public class MessageMap : EntityMap<Message>
{
    public MessageMap()
    {
        Map(x => x.Text).ToColumn("MessageText", false);
        Map(x => x.FileKey).ToColumn("FileKey", false);
    }
}