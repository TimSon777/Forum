using FluentMigrator;

namespace Chat.Migrator.Migrations;

[Migration(2)]
// ReSharper disable once UnusedType.Global
public class Migration2AddUserIdToMessage : Migration
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