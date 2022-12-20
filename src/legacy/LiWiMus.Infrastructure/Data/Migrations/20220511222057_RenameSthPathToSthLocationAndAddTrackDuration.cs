using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class RenameSthPathToSthLocationAndAddTrackDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Playlists",
                newName: "PhotoLocation");

            migrationBuilder.RenameColumn(
                name: "AvatarPath",
                table: "AspNetUsers",
                newName: "AvatarLocation");

            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Artists",
                newName: "PhotoLocation");

            migrationBuilder.RenameColumn(
                name: "CoverPath",
                table: "Albums",
                newName: "CoverLocation");

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "Tracks",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Tracks");

            migrationBuilder.RenameColumn(
                name: "PhotoLocation",
                table: "Playlists",
                newName: "PhotoPath");

            migrationBuilder.RenameColumn(
                name: "AvatarLocation",
                table: "AspNetUsers",
                newName: "AvatarPath");

            migrationBuilder.RenameColumn(
                name: "PhotoLocation",
                table: "Artists",
                newName: "PhotoPath");

            migrationBuilder.RenameColumn(
                name: "CoverLocation",
                table: "Albums",
                newName: "CoverPath");
        }
    }
}
