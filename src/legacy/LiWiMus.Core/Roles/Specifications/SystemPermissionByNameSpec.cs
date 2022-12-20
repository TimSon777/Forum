using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Roles.Specifications;

public sealed class SystemPermissionByNameSpec : Specification<SystemPermission>, ISingleResultSpecification
{
    public SystemPermissionByNameSpec(string name)
    {
        Query.Where(permission => permission.Name == name);
    }
}

public static partial class SystemPermissionRepositoryExtensions
{
    public static async Task<SystemPermission?> GetByNameAsync(this IRepository<SystemPermission> repository,
                                                               string name)
    {
        var spec = new SystemPermissionByNameSpec(name);
        return await repository.GetBySpecAsync(spec);
    }
}