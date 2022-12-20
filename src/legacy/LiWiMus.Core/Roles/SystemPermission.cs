using System.Security.Claims;

namespace LiWiMus.Core.Roles;

public class SystemPermission : BaseEntity
{
    public const string ClaimType = "sysperm";

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<Role> Roles { get; set; } = new();

    public Claim GetClaim()
    {
        return new Claim(ClaimType, Name);
    }
}