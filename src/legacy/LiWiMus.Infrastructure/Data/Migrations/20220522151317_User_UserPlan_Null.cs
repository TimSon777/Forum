using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class User_UserPlan_Null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserPlan_UserPlanId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserPlanId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserPlan_UserPlanId",
                table: "AspNetUsers",
                column: "UserPlanId",
                principalTable: "UserPlan",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserPlan_UserPlanId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserPlanId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserPlan_UserPlanId",
                table: "AspNetUsers",
                column: "UserPlanId",
                principalTable: "UserPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
