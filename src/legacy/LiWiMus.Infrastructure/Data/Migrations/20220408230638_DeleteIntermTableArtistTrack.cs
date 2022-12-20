using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class DeleteIntermTableArtistTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTrack_Artists_ArtistId",
                table: "ArtistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTrack_Tracks_TrackId",
                table: "ArtistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistTrack",
                table: "ArtistTrack");

            migrationBuilder.DropIndex(
                name: "IX_ArtistTrack_ArtistId",
                table: "ArtistTrack");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ArtistTrack");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ArtistTrack");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "ArtistTrack");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "ArtistTrack",
                newName: "TracksId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "ArtistTrack",
                newName: "OwnersId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistTrack_TrackId",
                table: "ArtistTrack",
                newName: "IX_ArtistTrack_TracksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistTrack",
                table: "ArtistTrack",
                columns: new[] { "OwnersId", "TracksId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTrack_Artists_OwnersId",
                table: "ArtistTrack",
                column: "OwnersId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTrack_Tracks_TracksId",
                table: "ArtistTrack",
                column: "TracksId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTrack_Artists_OwnersId",
                table: "ArtistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistTrack_Tracks_TracksId",
                table: "ArtistTrack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistTrack",
                table: "ArtistTrack");

            migrationBuilder.RenameColumn(
                name: "TracksId",
                table: "ArtistTrack",
                newName: "TrackId");

            migrationBuilder.RenameColumn(
                name: "OwnersId",
                table: "ArtistTrack",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistTrack_TracksId",
                table: "ArtistTrack",
                newName: "IX_ArtistTrack_TrackId");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Tracks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ArtistTrack",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ArtistTrack",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "ArtistTrack",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistTrack",
                table: "ArtistTrack",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_AlbumId",
                table: "Tracks",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistTrack_ArtistId",
                table: "ArtistTrack",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTrack_Artists_ArtistId",
                table: "ArtistTrack",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistTrack_Tracks_TrackId",
                table: "ArtistTrack",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Albums_AlbumId",
                table: "Tracks",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
