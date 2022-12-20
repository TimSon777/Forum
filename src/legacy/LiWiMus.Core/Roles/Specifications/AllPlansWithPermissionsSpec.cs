using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Roles.Specifications;

public sealed class AllRolesWithPermissionsSpec : Specification<Role>
{
    public AllRolesWithPermissionsSpec()
    {
        Query.Include(role => role.Permissions);
    }
}

public static partial class RoleRepositoryExtensions
{
    public static async Task<List<Role>> GetAllAsync(this IRepository<Role> repository)
    {
        var spec = new AllRolesWithPermissionsSpec();
        return await repository.ListAsync(spec);
    }
}