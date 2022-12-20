using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Roles.Specifications;

public sealed class RolesByUserSpec : Specification<Role>
{
    public RolesByUserSpec(User user)
    {
        Query.Where(role => role.Users.Any(u => u.Id == user.Id));
        Query.Include(role => role.Permissions);
    }
}

public static partial class RoleRepositoryExtensions
{
    public static async Task<List<Role>> GetByUserAsync(this IRepository<Role> repository, User user)
    {
        var rolesByUserSpec = new RolesByUserSpec(user);
        return await repository.ListAsync(rolesByUserSpec) ?? new List<Role>();
    }
}