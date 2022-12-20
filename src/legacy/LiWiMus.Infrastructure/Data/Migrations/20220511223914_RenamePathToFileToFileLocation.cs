using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class RenamePathToFileToFileLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PathToFile",
                table: "Tracks",
                newName: "FileLocation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileLocation",
                table: "Tracks",
                newName: "PathToFile");
        }
    }
}
