namespace Chat.Infrastructure.Database;

public static class Naming
{
    public static class User
    {
        public const string TableName = "Users";
        public const string PrimaryKey = "Id";
        public const string Name = nameof(Name);
    }
    
    public static class Message
    {
        public const string TableName = "Messages";
        public const string PrimaryKey = "Id";
        public const string Text = nameof(Text);
    }
}