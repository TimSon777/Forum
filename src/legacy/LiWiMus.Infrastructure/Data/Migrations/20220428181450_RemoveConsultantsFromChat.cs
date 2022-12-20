using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class RemoveConsultantsFromChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_OnlineConsultants_ConsultantId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_ConsultantId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "ConsultantId",
                table: "Chat");

            migrationBuilder.AddColumn<string>(
                name: "ConsultantConnectionId",
                table: "Chat",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "OnlineConsultantId",
                table: "Chat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_OnlineConsultantId",
                table: "Chat",
                column: "OnlineConsultantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_OnlineConsultants_OnlineConsultantId",
                table: "Chat",
                column: "OnlineConsultantId",
                principalTable: "OnlineConsultants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_OnlineConsultants_OnlineConsultantId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_OnlineConsultantId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "ConsultantConnectionId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "OnlineConsultantId",
                table: "Chat");

            migrationBuilder.AddColumn<int>(
                name: "ConsultantId",
                table: "Chat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_ConsultantId",
                table: "Chat",
                column: "ConsultantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_OnlineConsultants_ConsultantId",
                table: "Chat",
                column: "ConsultantId",
                principalTable: "OnlineConsultants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
