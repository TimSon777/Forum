namespace LiWiMus.Core.Roles;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public List<SystemPermission> Permissions { get; set; } = new();
    public List<User> Users { get; set; } = new();
}