using Dapper.FluentMap.Mapping;
using Forum.DAL.Abstractions.Chat.Data;

namespace Forum.DAL.Implementations.Database.Maps;

public class MessageMap : EntityMap<GetMessageItemStorage>
{
    public MessageMap()
    {
        Map(x => x.Text).ToColumn("MessageText", false);
        Map(x => x.FileKey).ToColumn("FileKey", false);
    }
}