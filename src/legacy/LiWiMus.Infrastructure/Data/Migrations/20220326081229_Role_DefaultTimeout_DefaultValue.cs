using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class Role_DefaultTimeout_DefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DefaultTimeout",
                table: "AspNetRoles",
                type: "varchar(48)",
                nullable: false,
                defaultValue: "10675199.02:48:05.4775807",
                oldClrType: typeof(string),
                oldType: "varchar(48)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DefaultTimeout",
                table: "AspNetRoles",
                type: "varchar(48)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(48)",
                oldDefaultValue: "10675199.02:48:05.4775807")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
