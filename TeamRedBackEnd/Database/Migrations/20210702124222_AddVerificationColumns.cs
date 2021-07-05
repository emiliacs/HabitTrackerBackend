using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamRedBackEnd.Migrations
{
    public partial class AddVerificationColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_verification_code",
                table: "userProfile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "user_verified",
                table: "userProfile",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_verification_code",
                table: "userProfile");

            migrationBuilder.DropColumn(
                name: "user_verified",
                table: "userProfile");
        }
    }
}
