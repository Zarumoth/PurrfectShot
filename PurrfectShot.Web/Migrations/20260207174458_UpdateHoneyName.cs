using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrfectShot.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHoneyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Хъни-Бъни");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Хъни");
        }
    }
}
