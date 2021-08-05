using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamRedBackEnd.Migrations
{
    public partial class AddRelation_OwnerIdToHabitHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "userProfile",
                newName: "user_salt");

            migrationBuilder.RenameColumn(
                name: "BytePassword",
                table: "userProfile",
                newName: "user_byte_password");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "history",
                newName: "history_owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_history_owner_id",
                table: "history",
                column: "history_owner_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ownerId",
                table: "history",
                column: "history_owner_id",
                principalTable: "userProfile",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ownerId",
                table: "history");

            migrationBuilder.DropIndex(
                name: "IX_history_history_owner_id",
                table: "history");

            migrationBuilder.RenameColumn(
                name: "user_salt",
                table: "userProfile",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "user_byte_password",
                table: "userProfile",
                newName: "BytePassword");

            migrationBuilder.RenameColumn(
                name: "history_owner_id",
                table: "history",
                newName: "OwnerId");
        }
    }
}
