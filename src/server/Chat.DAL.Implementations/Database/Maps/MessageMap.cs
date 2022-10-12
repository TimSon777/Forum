using Chat.DAL.Abstractions.Chat.Data;
using Dapper.FluentMap.Mapping;

namespace Chat.DAL.Implementations.Database.Maps;

public class MessageMap : EntityMap<GetMessageItemStorage>
{
    public MessageMap()
    {
        Map(x => x.Text).ToColumn("MessageText", false);
    }
}