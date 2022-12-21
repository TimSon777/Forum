namespace Chat.Migrator.Migrations;

[TimestampedMigration(2022, 12, 21, 19, 12)]
public sealed class AddSupportChat : ForwardOnlyMigration
{
    public override void Up()
    {
        Create
            .Column("IsAdmin")
            .OnTable("Users")
            .AsBoolean()
            .NotNullable();

        Create
            .Table("Connections")
            .WithColumn("ConnectionId").AsString().NotNullable()
            .WithColumn("UserId").AsInt32().ForeignKey().Indexed();

        Create
            .Column("MateId")
            .OnTable("Users")
            .AsInt32()
            .ForeignKey()
            .Indexed();

        Create
            .Column("UserFromId")
            .OnTable("Messages")
            .AsInt32()
            .NotNullable()
            .ForeignKey()
            .Indexed();
        
        Create
            .Column("UserToId")
            .OnTable("Messages")
            .AsInt32()
            .Nullable()
            .ForeignKey()
            .Indexed();
    }
}