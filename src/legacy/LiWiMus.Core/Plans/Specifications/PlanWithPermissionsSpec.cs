using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class PlanWithPermissionsSpec : Specification<Plan>, ISingleResultSpecification
{
    public PlanWithPermissionsSpec(int id)
    {
        Query.Where(plan => plan.Id == id);
        Query.Include(plan => plan.Permissions);
    }
}

public static partial class PlanRepositoryExtensions
{
    public static async Task<Plan?> GetWithPermissionsAsync(this IRepository<Plan> repository, int id)
    {
        var spec = new PlanWithPermissionsSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}