namespace LiWiMus.Core.Roles.Exceptions;

public class UserNotInRoleException : InvalidOperationException
{
    public UserNotInRoleException(User user, Role role) : base($"User '{user.UserName}' not in role '{role.Name}'")
    {
    }
}