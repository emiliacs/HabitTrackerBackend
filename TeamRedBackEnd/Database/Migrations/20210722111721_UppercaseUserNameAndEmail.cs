using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamRedBackEnd.Migrations
{
    public partial class UppercaseUserNameAndEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_userProfile_user_email",
                table: "userProfile");

            migrationBuilder.DropIndex(
                name: "IX_userProfile_user_name",
                table: "userProfile");

            migrationBuilder.DropIndex(
                name: "IX_userProfile_user_name_user_email",
                table: "userProfile");

            migrationBuilder.AddColumn<string>(
                name: "user_email_upper",
                table: "userProfile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_name_upper",
                table: "userProfile",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userProfile_user_email_upper",
                table: "userProfile",
                column: "user_email_upper",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userProfile_user_name_upper",
                table: "userProfile",
                column: "user_name_upper",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userProfile_user_name_upper_user_email_upper",
                table: "userProfile",
                columns: new[] { "user_name_upper", "user_email_upper" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_userProfile_user_email_upper",
                table: "userProfile");

            migrationBuilder.DropIndex(
                name: "IX_userProfile_user_name_upper",
                table: "userProfile");

            migrationBuilder.DropIndex(
                name: "IX_userProfile_user_name_upper_user_email_upper",
                table: "userProfile");

            migrationBuilder.DropColumn(
                name: "user_email_upper",
                table: "userProfile");

            migrationBuilder.DropColumn(
                name: "user_name_upper",
                table: "userProfile");

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

            migrationBuilder.CreateIndex(
                name: "IX_userProfile_user_name_user_email",
                table: "userProfile",
                columns: new[] { "user_name", "user_email" },
                unique: true);
        }
    }
}
