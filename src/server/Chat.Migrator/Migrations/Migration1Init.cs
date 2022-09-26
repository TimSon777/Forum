using Chat.Infrastructure.Database;
using FluentMigrator;

namespace Chat.Migrator.Migrations;

[Migration(1)]
// ReSharper disable once UnusedType.Global
public class Migration1Init : Migration
{
    public override void Up()
    {
        Create
            .Table(Naming.User.TableName)
            
            .WithColumn(Naming.User.PrimaryKey)
            .AsInt32()
            .PrimaryKey()
            .Identity()
            
            .WithColumn(Naming.User.Name)
            .AsString(20)
            .Unique();
        
        Create
            .Table(Naming.Message.TableName)
            
            .WithColumn(Naming.Message.PrimaryKey)
            .AsInt64()
            .PrimaryKey()
            .Identity()
            
            .WithColumn(Naming.Message.Text)
            .AsString(500);
    }

    public override void Down()
    {
        Delete.Table(Naming.User.TableName);
        Delete.Table(Naming.Message.TableName);
    }
}