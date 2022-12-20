using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class PermissionsByPlanSpec : Specification<Permission>
{
    public PermissionsByPlanSpec(Plan plan)
    {
        Query.Where(permission => permission.Plans.Any(p => p.Name == plan.Name));
        Query.Include(permission => permission.Plans);
    }

    public PermissionsByPlanSpec(string planName)
    {
        Query.Where(permission => permission.Plans.Any(p => p.Name == planName));
        Query.Include(permission => permission.Plans);
    }
}

public static partial class PermissionRepositoryExtensions
{
    public static async Task<List<Permission>> GetByPlanAsync(this IRepository<Permission> repository, Plan plan)
    {
        var spec = new PermissionsByPlanSpec(plan);
        return await repository.ListAsync(spec);
    }

    public static async Task<List<Permission>> GetByPlanAsync(this IRepository<Permission> repository, string planName)
    {
        var spec = new PermissionsByPlanSpec(planName);
        return await repository.ListAsync(spec);
    }
}