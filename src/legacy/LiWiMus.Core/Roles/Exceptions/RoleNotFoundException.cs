using Ardalis.GuardClauses;

namespace LiWiMus.Core.Roles.Exceptions;

public class RoleNotFoundException : NotFoundException
{
    public RoleNotFoundException(string roleName) : base(roleName, "Role")
    {
    }

    public RoleNotFoundException(string roleName, Exception innerException) : base(roleName, "Role", innerException)
    {
    }
}