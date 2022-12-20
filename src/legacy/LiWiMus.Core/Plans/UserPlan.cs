using System.Linq.Expressions;

namespace LiWiMus.Core.Plans;

public class UserPlan : BaseEntity
{
    public User User { get; set; } = null!;
    public Plan Plan { get; set; } = null!;

    public int UserId { get; set; }
    public int PlanId { get; set; }

    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public bool Updatable => End > DateTime.UtcNow && DefaultPlans.Default.Name != Plan.Name;

    public static readonly Expression<Func<UserPlan, bool>> IsActive = plan => plan.End > DateTime.UtcNow;
    public static readonly Expression<Func<UserPlan, bool>> IsNotActive = plan => plan.End <= DateTime.UtcNow;
}