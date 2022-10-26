using FluentMigrator;

namespace Chat.Migrator.Migrations;

[Migration(2022_10_26__17_45)]
// ReSharper disable once UnusedType.Global
public class Migration_202210261745_Init : Migration
{
    public override void Up()
    {
        Create
            .Table("Users")
            
            .WithColumn("Id")
            .AsInt32()
            .PrimaryKey()
            .Identity()
            
            .WithColumn("Name")
            .AsString(20)
            .Unique();
        
        Create
            .Table("Messages")
            
            .WithColumn("Id")
            .AsInt64()
            .PrimaryKey()
            .Identity()
            
            .WithColumn("Text")
            .AsString(500);
    }

    public override void Down()
    {
        Delete.Table("Users");
        Delete.Table("Messages");
    }
}