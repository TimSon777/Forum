using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class UserPlansSearchSpec : Specification<UserPlan>
{
    public UserPlansSearchSpec(int? userId, int? planId, bool? active)
    {
        Query.Where(up => up.UserId == userId, userId is not null)
             .Where(up => up.PlanId == planId, planId is not null)
             .Where(UserPlan.IsActive, active is not null && active.Value)
             .Where(UserPlan.IsNotActive, active is not null && !active.Value)
             .Include(up => up.Plan)
             .Include(up => up.User);
    }
}

public static partial class UserPlanRepositoryExtensions
{
    public static async Task<List<UserPlan>> SearchAsync(this IRepository<UserPlan> repository, int? userId,
                                                         int? planId, bool? active)
    {
        var spec = new UserPlansSearchSpec(userId, planId, active);
        return await repository.ListAsync(spec);
    }
}