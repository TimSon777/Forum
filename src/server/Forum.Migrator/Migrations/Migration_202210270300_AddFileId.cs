namespace Chat.Migrator.Migrations;

[Migration(2022_10_27_03_00)]
public sealed class Migration_202210270300_AddFileId : Migration
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