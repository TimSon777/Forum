namespace LiWiMus.Core.Roles;

public static class DefaultRoles
{
    public static class User
    {
        public const string Name = nameof(User);
        public const string Description = "Basic role";
    }

    public static class Admin
    {
        public const string Name = nameof(Admin);
        public const string Description = "Administration role";
    }

    public static class Consultant
    {
        public const string Name = nameof(Consultant);
        public const string Description = "Consultant role";
    }

    public static IEnumerable<Role> GetAll()
    {
        yield return new Role
        {
            Name = User.Name,
            Description = User.Description
        };

        yield return new Role
        {
            Name = Consultant.Name,
            Description = Consultant.Description
        };

        yield return new Role
        {
            Name = Admin.Name,
            Description = Admin.Description
        };
    }
}