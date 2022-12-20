using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Plans.Specifications;

public sealed class ActiveUserPlansSpec : Specification<UserPlan>
{
    public ActiveUserPlansSpec(Plan plan)
    {
        Query.Where(up => up.PlanId == plan.Id)
             .Where(UserPlan.IsActive)
             .Include(up => up.User)
             .Include(up => up.Plan.Permissions);
    }

    public ActiveUserPlansSpec(User user)
    {
        Query.Where(up => up.UserId == user.Id)
             .Where(UserPlan.IsActive)
             .Include(up => up.User)
             .Include(up => up.Plan.Permissions);
    }
}

public static partial class UserPlanRepositoryExtensions
{
    public static async Task<List<UserPlan>> GetActiveAsync(this IRepository<UserPlan> repository, Plan plan)
    {
        var spec = new ActiveUserPlansSpec(plan);
        return await repository.ListAsync(spec);
    }

    public static async Task<List<UserPlan>> GetActiveAsync(this IRepository<UserPlan> repository, User user)
    {
        var spec = new ActiveUserPlansSpec(user);
        return await repository.ListAsync(spec);
    }

    public static async Task<bool> HasActiveAsync(this IRepository<UserPlan> repository, Plan plan)
    {
        var spec = new ActiveUserPlansSpec(plan);
        return await repository.AnyAsync(spec);
    }

    public static async Task<bool> HasActiveAsync(this IRepository<UserPlan> repository, User user)
    {
        var spec = new ActiveUserPlansSpec(user);
        return await repository.AnyAsync(spec);
    }
}