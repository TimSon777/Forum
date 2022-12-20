using Ardalis.GuardClauses;

namespace LiWiMus.Core.Plans.Exceptions;

public class PermissionNotFoundException : NotFoundException
{
    public PermissionNotFoundException(string name) : base(name, "Permission")
    {
    }

    public PermissionNotFoundException(string name, Exception innerException) : base(name, "Permission", innerException)
    {
    }
}