using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TeamRedBackEnd.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userProfile",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    user_email = table.Column<string>(type: "text", nullable: true),
                    user_password = table.Column<string>(type: "text", nullable: true),
                    user_picture = table.Column<string>(type: "text", nullable: true),
                    user_public_profile = table.Column<bool>(type: "boolean", nullable: false),
                    user_friends = table.Column<int[]>(type: "integer[]", nullable: true),
                    user_groups = table.Column<int[]>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userProfile", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "habit",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    habit_id = table.Column<int>(type: "integer", nullable: false),
                    habit_owner_id = table.Column<int>(type: "integer", nullable: false),
                    habit_name = table.Column<string>(type: "text", nullable: true),
                    habit_description = table.Column<string>(type: "text", nullable: true),
                    habit_reward = table.Column<string>(type: "text", nullable: true),
                    habit_start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    habit_end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    habit_times_todo = table.Column<int>(type: "integer", nullable: false),
                    habit_day_repeat = table.Column<int>(type: "integer", nullable: false),
                    habit_timespan = table.Column<int>(type: "integer", nullable: false),
                    habit_category = table.Column<int>(type: "integer", nullable: false),
                    habit_favourite = table.Column<bool>(type: "boolean", nullable: false),
                    habit_public_habit = table.Column<bool>(type: "boolean", nullable: false),
                    habit_weekdays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_habit", x => x.id);
                    table.ForeignKey(
                        name: "FK_habit_owner_Id",
                        column: x => x.habit_owner_id,
                        principalTable: "userProfile",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_habit = table.Column<int>(type: "integer", nullable: false),
                    group_user_ids = table.Column<int[]>(type: "integer[]", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.group_id);
                    table.ForeignKey(
                        name: "FK_group_userProfile_UserId",
                        column: x => x.UserId,
                        principalTable: "userProfile",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_habit_group_owner_Id",
                        column: x => x.group_habit,
                        principalTable: "habit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HabitId = table.Column<int>(type: "integer", nullable: false),
                    history_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    history_result_bool = table.Column<bool>(type: "boolean", nullable: false),
                    history_result_num = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_habitId",
                        column: x => x.HabitId,
                        principalTable: "habit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_group_group_habit",
                table: "group",
                column: "group_habit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_group_UserId",
                table: "group",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_habit_habit_owner_id",
                table: "habit",
                column: "habit_owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_HabitId",
                table: "history",
                column: "HabitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "habit");

            migrationBuilder.DropTable(
                name: "userProfile");
        }
    }
}
