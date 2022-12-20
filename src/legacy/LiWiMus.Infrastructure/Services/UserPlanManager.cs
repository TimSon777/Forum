using LiWiMus.Core.Plans;
using LiWiMus.Core.Plans.Exceptions;
using LiWiMus.Core.Plans.Interfaces;
using LiWiMus.Core.Plans.Specifications;
using LiWiMus.Core.Transactions;
using LiWiMus.Core.Users;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Infrastructure.Services;

public class UserPlanManager : IUserPlanManager
{
    private readonly IRepository<UserPlan> _userPlanRepository;
    private readonly IRepository<Plan> _planRepository;
    private readonly IRepository<Transaction> _transactionRepository;

    public UserPlanManager(IRepository<UserPlan> userPlanRepository, IRepository<Plan> planRepository,
                           IRepository<Transaction> transactionRepository)
    {
        _userPlanRepository = userPlanRepository;
        _planRepository = planRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<UserPlan> AddToDefaultPlanAsync(User user)
    {
        var defaultPlan = await _planRepository.GetBySpecAsync(new PlanByNameSpec(DefaultPlans.Default.Name));

        if (defaultPlan is null)
        {
            throw new InvalidOperationException("Default plan not added to database");
        }

        return await AddToPlanAsync(user, defaultPlan, DateTime.UtcNow, new DateTime(2099, 12, 31));
    }

    public async Task<UserPlan> UpdateUserPlanAsync(UserPlan userPlan)
    {
        var plan = userPlan.Plan;
        if (plan.Name == DefaultPlans.Default.Name)
        {
            throw new DefaultUserPlanIsReadonlyException();
        }

        await _userPlanRepository.UpdateAsync(userPlan);
        return userPlan;
    }

    public async Task<bool> IsInPlanAsync(User user, Plan plan)
    {
        var userPlan = await _userPlanRepository.GetActiveAsync(user, plan);
        return userPlan is not null;
    }

    public async Task BuyPlanAsync(User user, Plan plan, TimeSpan time)
    {
        var amount = plan.PricePerMonth * (decimal) time.TotalDays / 30;
        if (user.Balance < amount)
        {
            throw new UserDoesntHaveEnoughMoneyException();
        }

        await AddToPlanAsync(user, plan, DateTime.UtcNow, DateTime.UtcNow.Add(time));
        var transaction = new Transaction
        {
            User = user,
            Amount = -amount,
            Description = $"Buying a plan '{plan.Name}'"
        };
        await _transactionRepository.AddAsync(transaction);
    }

    public async Task<UserPlan> AddToPlanAsync(User user, Plan plan, DateTime start, DateTime end)
    {
        if (end < DateTime.UtcNow)
        {
            throw new ArgumentException("Must be in future", nameof(end));
        }

        if (start > end)
        {
            throw new ArgumentException("Must be less than end date", nameof(start));
        }

        var activeUserPlans = await _userPlanRepository.SearchAsync(user.Id, plan.Id, true);
        if (activeUserPlans.Count > 0)
        {
            throw new UserAlreadyOwnsPlanException(user, plan);
        }

        var userPlan = CreateUserPlan(user, plan, start, end);
        await _userPlanRepository.AddAsync(userPlan);
        return userPlan;
    }

    private static UserPlan CreateUserPlan(User user, Plan plan, DateTime start, DateTime end)
    {
        return new UserPlan
        {
            User = user,
            Plan = plan,
            Start = start,
            End = end
        };
    }
}