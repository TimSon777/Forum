using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Users.Specifications;

public sealed class UserFollowingByUserIdSpec : Specification<User>
{
    public UserFollowingByUserIdSpec(int userId)
    {
        Query.Where(u => u.Followers.Any(x => x.Follower.Id == userId));
    }
}

public static partial class UsersRepositoryExtensions
{
    public static async Task<List<User>> FollowingAsync(this IRepository<User> repository, int userId)
    {
        var spec = new UserFollowingByUserIdSpec(userId);
        return await repository.ListAsync(spec);
    }
}