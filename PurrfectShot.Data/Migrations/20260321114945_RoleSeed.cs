using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PurrfectShot.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f1e2d3c-4b5a-6789-0123-456789abcdef", null, "Friend", "FRIEND" },
                    { "f1a2b3c4-d5e6-7890-1234-56789abcdef0", null, "FamilyMember", "FAMILYMEMBER" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "38058665-8726-41fa-be91-41de9acd0f72",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ddea49b-582e-4ab9-a109-99f139953b8d", "AQAAAAIAAYagAAAAEDizdzNxC9VndM0Rvoz9dkET9M++BfJ6UZWj+igGuzsWRddc4aQRIJ5kb29YZ9/qXw==", "63a865d2-2d38-4f4a-9ca0-92b6535b1c90" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "0f1e2d3c-4b5a-6789-0123-456789abcdef");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f1a2b3c4-d5e6-7890-1234-56789abcdef0");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e",
                column: "ConcurrencyStamp",
                value: "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "38058665-8726-41fa-be91-41de9acd0f72",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72ca636c-e941-4712-b5ce-87dfb7dbac16", "AQAAAAIAAYagAAAAEHUMiyIufy5DQc2H9/yd8sDlvisUd0y/nkSyTifvf/IpK4qAe8mrhZXEBBxVcinNxg==", "46cefe64-399e-47b6-ab7e-585d7ed3658b" });
        }
    }
}
