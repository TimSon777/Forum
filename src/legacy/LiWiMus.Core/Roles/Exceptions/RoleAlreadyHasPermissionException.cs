namespace LiWiMus.Core.Roles.Exceptions;

public class RoleAlreadyHasPermissionException : InvalidOperationException
{
    public RoleAlreadyHasPermissionException(SystemPermission permission, Role role) : base(
        $"Role '{role.Name}' already has permission '{permission.Name}'")
    {
    }
}