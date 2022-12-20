using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Users.Specifications;

public sealed class IsUserFollowerSpec : Specification<User, bool>
{
    public IsUserFollowerSpec(int followerId, int followingId)
    {
        Query.Where(user => user.Id == followerId);
        Query.Select(user => user.Following.Any(f => f.Following.Id == followingId));

    }
}

public static partial class UsersRepositoryExtensions
{
    public static async Task<bool?> IsUserFollowAsync(this IRepository<User> repository, int followerId, int followingId)
    {
        var spec = new IsUserFollowerSpec(followerId, followingId);
        return await repository.GetBySpecAsync(spec);
    }
}