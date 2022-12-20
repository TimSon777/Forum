using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Roles.Specifications;

public sealed class RoleByNameSpec : Specification<Role>, ISingleResultSpecification
{
    public RoleByNameSpec(string roleName)
    {
        Query.Where(role => role.Name == roleName);
        Query.Include(role => role.Permissions)
             .Include(role => role.Users);
    }
}

public static partial class RoleRepositoryExtensions
{
    public static async Task<Role?> GetByNameAsync(this IRepository<Role> repository, string roleName)
    {
        var spec = new RoleByNameSpec(roleName);
        return await repository.GetBySpecAsync(spec);
    }
}