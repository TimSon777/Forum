namespace LiWiMus.Core.Plans.Exceptions;

public class PlanAlreadyHasPermissionException : InvalidOperationException
{
    public PlanAlreadyHasPermissionException(Plan plan, Permission permission) : base(
        $"Plan '{plan.Name}' already has permission '{permission.Name}'")
    {
    }
}