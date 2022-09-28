namespace Chat.DAL.Implementations.Database;

public static class Naming
{
    public static class User
    {
        public const string TableName = "\"Users\"";
        public const string PrimaryKey = "\"Id\"";
        public const string Name = "\"Name\"";
    }
    
    public static class Message
    {
        public const string TableName = "\"Messages\"";
        public const string PrimaryKey = "\"Id\"";
        public const string Text = "\"Text\"";
        public const string ForeignKeyUser = "\"UserId\"";
    }
}