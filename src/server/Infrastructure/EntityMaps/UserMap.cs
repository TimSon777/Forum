using Dapper.FluentMap.Mapping;
using Domain.Entities;

namespace Infrastructure.EntityMaps;

public class UserMap : EntityMap<User>
{
    public UserMap()
    {
        Map(x => x.Name).ToColumn("User", false);
    }
}