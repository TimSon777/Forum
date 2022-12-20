using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class PlanByNameSpec : Specification<Plan>, ISingleResultSpecification
{
    public PlanByNameSpec(string name)
    {
        Query.Where(plan => plan.Name == name);
        Query.Include(plan => plan.Permissions);
    }
}

public static partial class PlanRepositoryExtensions
{
    public static async Task<Plan?> GetByNameAsync(this IRepository<Plan> repository, string planName)
    {
        var spec = new PlanByNameSpec(planName);
        return await repository.GetBySpecAsync(spec);
    }
}