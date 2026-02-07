using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrfectShot.Web.Migrations
{
    /// <inheritdoc />
    public partial class CatModelUpdatedWithMainPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainPhotoId",
                table: "Cats",
                type: "int",
                nullable: true,
                comment: "The ID of the photo selected as the main profile picture for the cat.");

            migrationBuilder.UpdateData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 1,
                column: "MainPhotoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 2,
                column: "MainPhotoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 3,
                column: "MainPhotoId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 4,
                column: "MainPhotoId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Cats_MainPhotoId",
                table: "Cats",
                column: "MainPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_Photos_MainPhotoId",
                table: "Cats",
                column: "MainPhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_Photos_MainPhotoId",
                table: "Cats");

            migrationBuilder.DropIndex(
                name: "IX_Cats_MainPhotoId",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "MainPhotoId",
                table: "Cats");
        }
    }
}
