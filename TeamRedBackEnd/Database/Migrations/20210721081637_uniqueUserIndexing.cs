using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamRedBackEnd.Migrations
{
    public partial class uniqueUserIndexing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_userProfile_user_email",
                table: "userProfile",
                column: "user_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userProfile_user_name",
                table: "userProfile",
                column: "user_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_userProfile_user_email",
                table: "userProfile");

            migrationBuilder.DropIndex(
                name: "IX_userProfile_user_name",
                table: "userProfile");
        }
    }
}
