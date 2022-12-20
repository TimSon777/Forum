using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class ActiveUserPlanSpec : Specification<UserPlan>, ISingleResultSpecification
{
    public ActiveUserPlanSpec(User user, Plan plan)
    {
        Query.Where(up => up.UserId == user.Id && up.PlanId == plan.Id)
             .Where(UserPlan.IsActive)
             .Include(up => up.User)
             .Include(up => up.Plan);
    }

    public ActiveUserPlanSpec(int userId, int planId)
    {
        Query.Where(up => up.UserId == userId && up.PlanId == planId)
             .Where(UserPlan.IsActive)
             .Include(up => up.User)
             .Include(up => up.Plan);
    }
}

public static partial class UserPlanRepositoryExtensions
{
    public static async Task<UserPlan?> GetActiveAsync(this IRepository<UserPlan> repository, User user, Plan plan)
    {
        var spec = new ActiveUserPlanSpec(user, plan);
        return await repository.GetBySpecAsync(spec);
    }

    public static async Task<UserPlan?> GetActiveAsync(this IRepository<UserPlan> repository, int userId, int planId)
    {
        var spec = new ActiveUserPlanSpec(userId, planId);
        return await repository.GetBySpecAsync(spec);
    }
}