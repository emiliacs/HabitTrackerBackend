using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamRedBackEnd.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_habit_group_owner_Id",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_habit_owner_Id",
                table: "habit");

            migrationBuilder.AddColumn<byte[]>(
                name: "BytePassword",
                table: "userProfile",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "userProfile",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_group_owner_Id",
                table: "group",
                column: "group_habit",
                principalTable: "habit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owner_Id",
                table: "habit",
                column: "habit_owner_id",
                principalTable: "userProfile",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_group_owner_Id",
                table: "group");

            migrationBuilder.DropForeignKey(
                name: "FK_owner_Id",
                table: "habit");

            migrationBuilder.DropColumn(
                name: "BytePassword",
                table: "userProfile");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "userProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_habit_group_owner_Id",
                table: "group",
                column: "group_habit",
                principalTable: "habit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_habit_owner_Id",
                table: "habit",
                column: "habit_owner_id",
                principalTable: "userProfile",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
