namespace Chat.Migrator.Migrations;

[Migration(2022_11_03_03_00)]
public sealed class Migration_202211030300_ChangeTypeFileKeyToString : Migration
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