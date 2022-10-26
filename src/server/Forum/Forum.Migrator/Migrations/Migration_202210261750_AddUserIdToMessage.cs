using FluentMigrator;

namespace Chat.Migrator.Migrations;

[Migration(2022_10_26__17_50)]
// ReSharper disable once UnusedType.Global
public class Migration_202210261750_AddUserIdToMessage : Migration
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