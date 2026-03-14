using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrfectShot.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e", "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "38058665-8726-41fa-be91-41de9acd0f72",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72ca636c-e941-4712-b5ce-87dfb7dbac16", "38058665-8726-41fa-be91-41de9acd0f72", "AQAAAAIAAYagAAAAEHUMiyIufy5DQc2H9/yd8sDlvisUd0y/nkSyTifvf/IpK4qAe8mrhZXEBBxVcinNxg==", "46cefe64-399e-47b6-ab7e-585d7ed3658b" });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e", "38058665-8726-41fa-be91-41de9acd0f72" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e", "38058665-8726-41fa-be91-41de9acd0f72" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b91f0f0c-99a3-4315-9c87-6cdcc81d1a6e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "38058665-8726-41fa-be91-41de9acd0f72",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b61accf3-c237-43cc-9a0d-89666577b531", "admin@purrfect.com", "AQAAAAIAAYagAAAAEDI3I1Z8bKwa2hXYT7oqUex9KxoB4VeSdAFUH8gsnsZin+xWecG89JYRJkxBV+/I2A==", "eecf9be1-0b51-416b-9666-93224aab3a5e" });
        }
    }
}
