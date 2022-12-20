namespace LiWiMus.Core.Plans.Interfaces;

public interface IUserPlanManager
{
    Task<UserPlan> AddToPlanAsync(User user, Plan plan, DateTime start, DateTime end);
    Task<UserPlan> AddToDefaultPlanAsync(User user);

    Task<UserPlan> UpdateUserPlanAsync(UserPlan userPlan);

    Task<bool> IsInPlanAsync(User user, Plan plan);

    Task BuyPlanAsync(User user, Plan plan, TimeSpan time);
}