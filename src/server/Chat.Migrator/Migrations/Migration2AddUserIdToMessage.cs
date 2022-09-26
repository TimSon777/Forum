using Chat.Infrastructure.Database;
using FluentMigrator;

namespace Chat.Migrator.Migrations;

[Migration(2)]
// ReSharper disable once UnusedType.Global
public class Migration2AddUserIdToMessage : Migration
{
    public override void Up()
    {
        Create
            .Column(Naming.Message.ForeignKeyUser)
            .OnTable(Naming.Message.TableName)
            .AsInt64();
        
        Create
            .ForeignKey()
            .FromTable(Naming.Message.TableName)
            .ForeignColumn(Naming.Message.ForeignKeyUser)
            .ToTable(Naming.User.TableName)
            .PrimaryColumn(Naming.User.PrimaryKey);
    }

    public override void Down()
    {
        Delete
            .ForeignKey()
            .FromTable(Naming.Message.TableName)
            .ForeignColumn(Naming.Message.ForeignKeyUser);

        Delete
            .Column(Naming.Message.ForeignKeyUser)
            .FromTable(Naming.Message.TableName);
    }
}