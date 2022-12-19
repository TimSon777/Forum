namespace Chat.Migrator.Migrations;

[TimestampedMigration(2022, 10, 27, 3, 0)]
public sealed class AddFileId : Migration
{
    public override void Up()
    {
        Create
            .Column("FileKey")
            .OnTable("Messages")
            .AsGuid()
            .Nullable();
    }

    public override void Down()
    {
        Delete
            .Column("FileKey")
            .FromTable("Messages");
    }
}