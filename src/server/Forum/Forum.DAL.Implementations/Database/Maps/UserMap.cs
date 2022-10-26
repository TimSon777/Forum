using Dapper.FluentMap.Mapping;
using Forum.DAL.Abstractions.Chat.Data;

namespace Forum.DAL.Implementations.Database.Maps;

public class UserMap : EntityMap<GetUserItemStorage>
{
    public UserMap()
    {
        Map(x => x.Name).ToColumn("UserName", false);
    }
}