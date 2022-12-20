using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class AllPlansWithPermissionsSpec : Specification<Plan>
{
    public AllPlansWithPermissionsSpec()
    {
        Query.Include(plan => plan.Permissions);
    }
}

public static partial class PlanRepositoryExtensions
{
    public static async Task<List<Plan>> GetAllAsync(this IRepository<Plan> repository)
    {
        var spec = new AllPlansWithPermissionsSpec();
        return await repository.ListAsync(spec);
    }
}