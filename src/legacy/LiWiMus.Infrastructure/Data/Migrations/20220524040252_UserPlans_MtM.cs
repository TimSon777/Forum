using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class UserPlans_MtM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserPlan_UserPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserPlanId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserPlanId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserPlan_UserId",
                table: "UserPlan",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlan_AspNetUsers_UserId",
                table: "UserPlan",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlan_AspNetUsers_UserId",
                table: "UserPlan");

            migrationBuilder.DropIndex(
                name: "IX_UserPlan_UserId",
                table: "UserPlan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserPlan");

            migrationBuilder.AddColumn<int>(
                name: "UserPlanId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserPlanId",
                table: "AspNetUsers",
                column: "UserPlanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserPlan_UserPlanId",
                table: "AspNetUsers",
                column: "UserPlanId",
                principalTable: "UserPlan",
                principalColumn: "Id");
        }
    }
}
