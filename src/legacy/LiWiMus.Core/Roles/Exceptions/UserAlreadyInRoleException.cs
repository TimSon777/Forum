namespace LiWiMus.Core.Roles.Exceptions;

public class UserAlreadyInRoleException : InvalidOperationException
{
    public UserAlreadyInRoleException(User user, Role role) : base(
        $"User with id '{user.Id} already in role '{role.Name}'")
    {
    }
}