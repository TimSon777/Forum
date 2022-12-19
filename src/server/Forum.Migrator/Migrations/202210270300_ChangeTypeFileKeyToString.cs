namespace Chat.Migrator.Migrations;

[TimestampedMigration(2022, 11, 3, 3, 0)]
public sealed class ChangeTypeFileKeyToString : Migration
{
    public override void Up()
    {
        Alter
            .Column("FileKey")
            .OnTable("Messages")
            .AsString()
            .Nullable();
    }

    public override void Down()
    {
        Alter
            .Column("FileKey")
            .OnTable("Messages")
            .AsGuid()
            .Nullable();
    }
}