namespace LiWiMus.Core.Plans;

public class Plan : BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal PricePerMonth { get; set; }
    public string Description { get; set; } = null!;

    public List<Permission> Permissions { get; set; } = new();
    public List<UserPlan> UserPlans { get; set; } = new();
}