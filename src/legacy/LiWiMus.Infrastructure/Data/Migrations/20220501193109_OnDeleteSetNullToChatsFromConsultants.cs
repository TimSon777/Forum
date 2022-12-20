using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class OnDeleteSetNullToChatsFromConsultants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_OnlineConsultants_OnlineConsultantId",
                table: "Chat");

            migrationBuilder.AlterColumn<string>(
                name: "ConsultantConnectionId",
                table: "Chat",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_OnlineConsultants_OnlineConsultantId",
                table: "Chat",
                column: "OnlineConsultantId",
                principalTable: "OnlineConsultants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_OnlineConsultants_OnlineConsultantId",
                table: "Chat");

            migrationBuilder.UpdateData(
                table: "Chat",
                keyColumn: "ConsultantConnectionId",
                keyValue: null,
                column: "ConsultantConnectionId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ConsultantConnectionId",
                table: "Chat",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_OnlineConsultants_OnlineConsultantId",
                table: "Chat",
                column: "OnlineConsultantId",
                principalTable: "OnlineConsultants",
                principalColumn: "Id");
        }
    }
}
