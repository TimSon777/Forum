using Ardalis.GuardClauses;

namespace LiWiMus.Core.Roles.Exceptions;

public class SystemPermissionNotFoundException : NotFoundException
{
    public SystemPermissionNotFoundException(string permissionName) : base(permissionName, "SystemPermission")
    {
    }

    public SystemPermissionNotFoundException(string permissionName, Exception innerException) : base(permissionName,
        "SystemPermission", innerException)
    {
    }
}