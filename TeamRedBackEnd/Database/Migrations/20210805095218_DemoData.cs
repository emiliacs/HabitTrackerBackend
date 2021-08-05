using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamRedBackEnd.Migrations
{
    public partial class DemoData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "userProfile",
                columns: new[] { "user_id", "user_byte_password", "user_email", "user_friends", "user_groups", "user_name", "user_password", "user_picture", "user_public_profile", "user_salt", "user_email_upper", "user_name_upper", "user_verification_code" },
                values: new object[] { 99, new byte[] { 73, 49, 136, 183, 28, 166, 184, 48, 11, 209, 96, 227, 42, 230, 185, 146 }, "test@email.com", null, null, "Test", "STGItxymuDAL0WDjKua5kg==", null, false, new byte[] { 65, 91, 161, 36, 119, 215, 97, 204, 197, 48, 94, 58, 57, 135, 212, 25 }, "TEST@EMAIL.COM", "TEST", "44OT3DmJrUTVRMOUi1IRG" });

            migrationBuilder.InsertData(
                table: "habit",
                columns: new[] { "id", "habit_category", "habit_weekdays", "habit_day_repeat", "habit_description", "habit_end_date", "habit_favourite", "habit_id", "habit_name", "habit_owner_id", "habit_public_habit", "habit_reward", "habit_start_date", "habit_timespan", "habit_times_todo" },
                values: new object[,]
                {
                    { 201, 0, 0, 0, "Habit todo", new DateTime(2021, 11, 13, 12, 52, 18, 86, DateTimeKind.Local).AddTicks(7252), false, 201, "The Habit", 99, true, "...pat on back", new DateTime(2021, 7, 22, 12, 52, 18, 83, DateTimeKind.Local).AddTicks(7092), 0, 20 },
                    { 202, 0, 0, 0, "Zzz...", new DateTime(2021, 11, 13, 0, 0, 0, 0, DateTimeKind.Local), false, 202, "Wake up", 99, true, "Coffee", new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), 0, 20 },
                    { 203, 0, 0, 0, "You know, to stay alive", new DateTime(2021, 11, 13, 0, 0, 0, 0, DateTimeKind.Local), false, 203, "Breath", 99, true, "Staying alive", new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), 0, 200000 },
                    { 204, 0, 0, 0, "Doing the thing with brains", new DateTime(2021, 11, 13, 0, 0, 0, 0, DateTimeKind.Local), false, 204, "Think", 99, true, "Not seeming stupid", new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), 0, 300000 },
                    { 205, 0, 0, 0, "hungry ? eat : check again later", new DateTime(2021, 11, 13, 0, 0, 0, 0, DateTimeKind.Local), false, 205, "Check if hungry", 99, true, "Not starving", new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), 0, 300 }
                });

            migrationBuilder.InsertData(
                table: "history",
                columns: new[] { "history_id", "history_date", "history_result_num", "history_result_bool", "HabitId", "history_owner_id" },
                values: new object[,]
                {
                    { 279, new DateTime(2021, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), 1, true, 201, 99 },
                    { 297, new DateTime(2021, 8, 7, 0, 0, 0, 0, DateTimeKind.Local), 2, true, 201, 99 },
                    { 298, new DateTime(2021, 8, 6, 0, 0, 0, 0, DateTimeKind.Local), 3, true, 201, 99 },
                    { 300, new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Local), 4, true, 201, 99 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "habit",
                keyColumn: "id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "habit",
                keyColumn: "id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "habit",
                keyColumn: "id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "habit",
                keyColumn: "id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "history",
                keyColumn: "history_id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "history",
                keyColumn: "history_id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "history",
                keyColumn: "history_id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "history",
                keyColumn: "history_id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "habit",
                keyColumn: "id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "userProfile",
                keyColumn: "user_id",
                keyValue: 99);
        }
    }
}
