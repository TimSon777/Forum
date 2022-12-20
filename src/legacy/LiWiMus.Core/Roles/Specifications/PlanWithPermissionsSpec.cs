using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Roles.Specifications;

public sealed class RoleWithPermissionsSpec : Specification<Role>, ISingleResultSpecification
{
    public RoleWithPermissionsSpec(int id)
    {
        Query.Where(role => role.Id == id);
        Query.Include(role => role.Permissions);
    }
}

public static partial class RoleRepositoryExtensions
{
    public static async Task<Role?> GetWithPermissionsAsync(this IRepository<Role> repository, int id)
    {
        var spec = new RoleWithPermissionsSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}