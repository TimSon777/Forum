using Dapper.FluentMap.Mapping;
using Forum.Domain.Entities;

namespace Forum.Infrastructure.EntityMaps;

public class UserMap : EntityMap<User>
{
    public UserMap()
    {
        Map(x => x.Name).ToColumn("User", false);
    }
}