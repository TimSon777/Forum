namespace Chat.Migrator.Migrations;

[TimestampedMigration(2022, 10, 26, 17, 50)]
public class AddUserIdToMessage : Migration
{
    public override void Up()
    {
        Create
            .Column("UserId")
            .OnTable("Messages")
            .AsInt64();
        
        Create
            .ForeignKey()
            .FromTable("Messages")
            .ForeignColumn("UserId")
            .ToTable("Users")
            .PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete
            .ForeignKey()
            .FromTable("Messages")
            .ForeignColumn("UserId");

        Delete
            .Column("UserId")
            .FromTable("Messages");
    }
}