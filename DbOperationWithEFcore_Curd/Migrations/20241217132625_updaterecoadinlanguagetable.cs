using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbOperationWithEFcore_Curd.Migrations
{
    /// <inheritdoc />
    public partial class updaterecoadinlanguagetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Hindi INDIA", "Hindi" },
                    { 2, "English", "English" },
                    { 3, "khota", "khota" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
