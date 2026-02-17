using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AllocellaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Building", "Capacity", "CreatedAt", "Floor", "IsAvailable", "RoomName", "RoomNumber" },
                values: new object[,]
                {
                    { 1, "PS", 16, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6821), 5, true, "M-Sect Laboratorium", "05.13" },
                    { 2, "D4", 32, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6827), 2, true, "Agile Laboratorium", "C-203" },
                    { 3, "D4", 16, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6831), 1, true, "Storage", "A-101" },
                    { 4, "D4", 32, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6834), 3, true, "Network Laboratorium", "C-305" },
                    { 5, "D4", 32, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7010), 3, true, "Operating System Laboratorium", "C-305" },
                    { 6, "D4", 32, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7015), 2, false, "Computer Laboratorium", "C-205" },
                    { 7, "D4", 64, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7018), 2, true, "Classroom", "B-201" },
                    { 8, "SAW", 128, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7022), 10, true, "Big Classroom", "10.08" },
                    { 9, "D3", 160, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7028), 1, true, "Small Auditorium", "HH-101" },
                    { 10, "PS", 204, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7050), 6, true, "Big Auditorium", "06.06" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6317), "admin@allocella.com", "System Administrator", "$2a$11$Vmn4E0COpJY3LLb2p0C49uZtFY/eH8c.csaYzX0sWrCrHpSQ7SnH2", "admin" },
                    { 2, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6322), "eriktriayudaw@allocella.com", "Erik Triayuda Wijaya", "$2a$11$0BV408Lcwz2bvtnHLJq2ZeSl/upyl8NHNQmhV42DxfB0AqDyqY/TS", "student" },
                    { 3, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6325), "lepine@allocella.com", "Lepine Pauline", "$2a$11$0BV408Lcwz2bvtnHLJq2ZeSl/upyl8NHNQmhV42DxfB0AqDyqY/TS", "student" },
                    { 4, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6328), "ellen@allocella.com", "Dr. Ellen Lionhart S.Tr.Kom", "$2a$11$OLNjLBVUMGZllQWiJLbI2OD8FDZZBFe7xPSYA.3RUH6vnVJZ2RQ2O", "lecturer" },
                    { 5, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(6331), "robert@allocella.com", "Dr. Robert Oppenheimer Ka.Boom", "$2a$11$OLNjLBVUMGZllQWiJLbI2OD8FDZZBFe7xPSYA.3RUH6vnVJZ2RQ2O", "lecturer" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedAt", "EndTime", "Purpose", "RoomId", "StartTime", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7170), new DateTime(2026, 2, 24, 3, 0, 0, 0, DateTimeKind.Utc), "Cook", 3, new DateTime(2026, 2, 14, 7, 0, 0, 0, DateTimeKind.Utc), "rejected", new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7172), 5 },
                    { 2, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7180), new DateTime(2026, 2, 17, 8, 0, 0, 0, DateTimeKind.Utc), "Communal brief with a movie shooting crew", 6, new DateTime(2026, 2, 15, 5, 0, 0, 0, DateTimeKind.Utc), "approved", new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7181), 3 },
                    { 3, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7187), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "To declare the announcement that The Holy Grail war are now publicly known", 10, new DateTime(2026, 2, 14, 8, 0, 0, 0, DateTimeKind.Utc), "pending", new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7188), 4 },
                    { 4, new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7194), new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Community weekly meeting", 1, new DateTime(2026, 2, 12, 8, 0, 0, 0, DateTimeKind.Utc), "completed", new DateTime(2026, 2, 14, 16, 43, 32, 379, DateTimeKind.Utc).AddTicks(7195), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
