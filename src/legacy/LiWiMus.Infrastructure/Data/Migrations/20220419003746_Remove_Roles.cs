using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiWiMus.Infrastructure.Data.Migrations
{
    public partial class Remove_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveUntil",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "GrantedAt",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ActiveUntil",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "GrantedAt",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "DefaultTimeout",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "PricePerMonth",
                table: "AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveUntil",
                table: "AspNetUserRoles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "GrantedAt",
                table: "AspNetUserRoles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveUntil",
                table: "AspNetUserClaims",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "GrantedAt",
                table: "AspNetUserClaims",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DefaultTimeout",
                table: "AspNetRoles",
                type: "varchar(48)",
                nullable: false,
                defaultValue: "10675199.02:48:05.4775807")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "AspNetRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerMonth",
                table: "AspNetRoles",
                type: "decimal(65,30)",
                nullable: true);
        }
    }
}
