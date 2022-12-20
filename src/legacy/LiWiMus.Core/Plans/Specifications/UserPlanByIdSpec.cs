using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class UserPlanByIdSpec : Specification<UserPlan>, ISingleResultSpecification
{
    public UserPlanByIdSpec(int id)
    {
        Query.Where(up => up.Id == id)
             .Include(up => up.Plan)
             .Include(up => up.User);
    }
}

public static partial class UserPlanRepositoryExtensions
{
    public static async Task<UserPlan?> GetAsync(this IRepository<UserPlan> repository, int id)
    {
        var spec = new UserPlanByIdSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}