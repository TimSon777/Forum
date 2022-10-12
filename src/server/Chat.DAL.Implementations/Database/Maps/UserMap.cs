using Chat.DAL.Abstractions.Chat.Data;
using Dapper.FluentMap.Mapping;

namespace Chat.DAL.Implementations.Database.Maps;

public class UserMap : EntityMap<GetUserItemStorage>
{
    public UserMap()
    {
        Map(x => x.Name).ToColumn("UserName", false);
    }
}