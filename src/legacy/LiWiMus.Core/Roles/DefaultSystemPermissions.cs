namespace LiWiMus.Core.Roles;

public static class DefaultSystemPermissions
{
    public static class Admin
    {
        public static class Access
        {
            public const string Name = $"{nameof(Admin)}.{nameof(Access)}";
            public const string Description = "Gives access to the site for administrators";
        }
    }

    public static class Chat
    {
        public static class Answer
        {
            public const string Name = $"{nameof(Chat)}.{nameof(Answer)}";
            public const string Description = "Allows you to reply to customer messages";
        }
    }

    public static IEnumerable<SystemPermission> GetAll()
    {
        yield return new SystemPermission
        {
            Name = Admin.Access.Name,
            Description = Admin.Access.Description
        };

        yield return new SystemPermission
        {
            Name = Chat.Answer.Name,
            Description = Chat.Answer.Description
        };
    }
}