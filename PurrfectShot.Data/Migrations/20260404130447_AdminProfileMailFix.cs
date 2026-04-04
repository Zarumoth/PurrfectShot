using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrfectShot.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminProfileMailFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "38058665-8726-41fa-be91-41de9acd0f72",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97ccb774-085a-48e7-a9c7-4f72812912b7", "admin@purrfect.com", "AQAAAAIAAYagAAAAEIGBaeKUAeL/Wt+vKEOnmkFsPLn3aa8kf0aPwZbHJdQS2kOviCH8vOydhUUiQxH6zQ==", "52a18ec4-4f85-48a7-b855-20c0d592aafc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "38058665-8726-41fa-be91-41de9acd0f72",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ddea49b-582e-4ab9-a109-99f139953b8d", "38058665-8726-41fa-be91-41de9acd0f72", "AQAAAAIAAYagAAAAEDizdzNxC9VndM0Rvoz9dkET9M++BfJ6UZWj+igGuzsWRddc4aQRIJ5kb29YZ9/qXw==", "63a865d2-2d38-4f4a-9ca0-92b6535b1c90" });
        }
    }
}
