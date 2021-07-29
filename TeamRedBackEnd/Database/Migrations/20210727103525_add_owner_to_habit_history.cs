using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamRedBackEnd.Migrations
{
    public partial class add_owner_to_habit_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "history",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "history");
        }
    }
}
