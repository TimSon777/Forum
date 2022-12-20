using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class MakeMessageOwnerOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_OnlineConsultants_OnlineConsultantId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_OwnerId",
                table: "Message");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Message",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_OwnerId",
                table: "Message",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_OnlineConsultants_OnlineConsultantId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_OwnerId",
                table: "Message");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_OwnerId",
                table: "Message",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
