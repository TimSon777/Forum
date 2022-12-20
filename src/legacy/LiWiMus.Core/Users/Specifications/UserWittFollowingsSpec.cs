using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Users.Specifications;

public sealed class UserWittFollowingsSpec : Specification<User>, ISingleResultSpecification
{
    public UserWittFollowingsSpec(int id)
    {
        Query
            .Where(user => user.Id == id)
            .Include(user => user.Following)
            .ThenInclude(f => f.Following);
    }
}

public static partial class UsersRepositoryExtensions
{
    public static async Task<User?> WithFollowingsAsync(this IRepository<User> repository, int id)
    {
        var spec = new UserWittFollowingsSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}