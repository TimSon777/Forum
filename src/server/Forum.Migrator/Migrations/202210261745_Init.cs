namespace Chat.Migrator.Migrations;

[TimestampedMigration(2022, 10, 26, 17, 24)]
public sealed class Init : Migration
{
    public override void Up()
    {
        Create
            .Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(20).Unique();
        
        Create
            .Table("Messages")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Text").AsString(500);
    }

    public override void Down()
    {
        Delete.Table("Users");
        Delete.Table("Messages");
    }
}