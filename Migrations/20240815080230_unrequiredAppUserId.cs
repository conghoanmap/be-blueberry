using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blueberry.Migrations
{
    /// <inheritdoc />
    public partial class unrequiredAppUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bffa0c93-1bb9-4eb4-ba5a-09ee4ab90bab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f31cc880-c611-4c14-9e7d-8a82dfa9067e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30ffdf97-c081-4037-b96a-42e15cab2dc1", null, "User", "USER" },
                    { "d30da329-012a-46ba-a73a-96bfadf74aca", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30ffdf97-c081-4037-b96a-42e15cab2dc1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d30da329-012a-46ba-a73a-96bfadf74aca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bffa0c93-1bb9-4eb4-ba5a-09ee4ab90bab", null, "Admin", "ADMIN" },
                    { "f31cc880-c611-4c14-9e7d-8a82dfa9067e", null, "User", "USER" }
                });
        }
    }
}
