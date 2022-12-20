using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class AddConsultants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_ConsultantId",
                table: "Chat");

            migrationBuilder.AddColumn<string>(
                name: "UserConnectionId",
                table: "Chat",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OnlineConsultants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConsultantId = table.Column<int>(type: "int", nullable: false),
                    ConnectionId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineConsultants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlineConsultants_AspNetUsers_ConsultantId",
                        column: x => x.ConsultantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineConsultants_ConsultantId",
                table: "OnlineConsultants",
                column: "ConsultantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_OnlineConsultants_ConsultantId",
                table: "Chat",
                column: "ConsultantId",
                principalTable: "OnlineConsultants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_OnlineConsultants_ConsultantId",
                table: "Chat");

            migrationBuilder.DropTable(
                name: "OnlineConsultants");

            migrationBuilder.DropColumn(
                name: "UserConnectionId",
                table: "Chat");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_ConsultantId",
                table: "Chat",
                column: "ConsultantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
