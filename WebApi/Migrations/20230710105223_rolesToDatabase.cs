using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class rolesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a75a846-d923-4991-9b0d-94d47dd5ab3b", "fbeb75e8-f8c9-4122-934e-c6c0bf60bd97", "Editor", "EDITOR" },
                    { "b35417ee-298b-45a6-836d-9e8b049858ba", "52cb8f51-f854-4cd9-b264-5667eb9721cd", "Admin", "ADMIN" },
                    { "c04e4fdb-5561-4d04-a704-ae63dd74b191", "aa401a8b-675d-4b06-9d13-bed957b5fc9d", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a75a846-d923-4991-9b0d-94d47dd5ab3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b35417ee-298b-45a6-836d-9e8b049858ba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c04e4fdb-5561-4d04-a704-ae63dd74b191");
        }
    }
}
