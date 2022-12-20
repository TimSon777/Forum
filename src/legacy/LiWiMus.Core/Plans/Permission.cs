using System.Security.Claims;

namespace LiWiMus.Core.Plans;

public class Permission : BaseEntity
{
    public const string ClaimType = "planperm";
    
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<Plan> Plans { get; set; } = new();

    public Claim GetClaim()
    {
        return new Claim(ClaimType, Name);
    }
}