using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Active", "BirthDate", "CreatedAt", "Email", "IsAdmin", "Name", "Password", "UpdateAt" },
                values: new object[] { 0, true, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@cloudgame.com", true, "admin", "admingame", null });
        }
    }
}
