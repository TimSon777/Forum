using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class PermissionByNameSpec : Specification<Permission>, ISingleResultSpecification
{
    public PermissionByNameSpec(string name)
    {
        Query.Where(p => p.Name == name);
    }
}

public static partial class PermissionRepositoryExtensions
{
    public static async Task<Permission?> GetByNameAsync(this IRepository<Permission> repository, string name)
    {
        var spec = new PermissionByNameSpec(name);
        return await repository.GetBySpecAsync(spec);
    }
}