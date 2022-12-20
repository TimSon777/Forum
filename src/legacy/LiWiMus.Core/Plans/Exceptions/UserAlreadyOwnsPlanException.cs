namespace LiWiMus.Core.Plans.Exceptions;

public class UserAlreadyOwnsPlanException : InvalidOperationException
{
    public UserAlreadyOwnsPlanException(User user, Plan plan)
        : base($"User {user} already owns plan {plan.Name}")
    {
    }
}