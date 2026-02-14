using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PurrfectShot.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedFavoritesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "Users",
                comment: "Represents the signed into the application user and their attributes");

            migrationBuilder.CreateTable(
                name: "UserFavoritePhotos",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoritePhotos", x => new { x.UserId, x.PhotoId });
                    table.ForeignKey(
                        name: "FK_UserFavoritePhotos_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoritePhotos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Mapping table for the Many-to-Many relationship between Users and their Favorite Photos.");

            migrationBuilder.InsertData(
                table: "UserFavoritePhotos",
                columns: new[] { "PhotoId", "UserId" },
                values: new object[,]
                {
                    { new Guid("256e3cf7-f9ec-4f14-8361-e2e6fd2ce0e4"), "38058665-8726-41fa-be91-41de9acd0f72" },
                    { new Guid("80377db9-ed3a-4013-b325-64651c2a4b6c"), "38058665-8726-41fa-be91-41de9acd0f72" },
                    { new Guid("89bae15d-1c8e-4299-80ff-9c14578ab6ee"), "38058665-8726-41fa-be91-41de9acd0f72" },
                    { new Guid("b79cb330-da37-4a09-8eaf-bfde8951051f"), "38058665-8726-41fa-be91-41de9acd0f72" },
                    { new Guid("f86bfb53-eb39-4770-94f5-35a4710d037f"), "38058665-8726-41fa-be91-41de9acd0f72" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "38058665-8726-41fa-be91-41de9acd0f72",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a34528f3-dbd6-4374-8a24-72c0919e4afd", "AQAAAAIAAYagAAAAEJRSLVBuCj9cenHL2zvwnOuxsu5XsTyQvQzhVEatcCoRvM+hLIh8evj183ptqnVDrA==", "fab4b11d-77dc-458d-8729-f0f541176979" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoritePhotos_PhotoId",
                table: "UserFavoritePhotos",
                column: "PhotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoritePhotos");

            migrationBuilder.AlterTable(
                name: "Users",
                oldComment: "Represents the signed into the application user and their attributes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "38058665-8726-41fa-be91-41de9acd0f72",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2732508c-2754-4372-8c1a-74bf753ce019", "AQAAAAIAAYagAAAAEKZhyatLgzuyt99I6A4UXRwlS2mnxTGwmt9xSQtyt8bAenSyVu9i5+d+ZjZK83jDRA==", "d656430a-27ae-485e-9c2b-bd230d89c5c3" });
        }
    }
}
