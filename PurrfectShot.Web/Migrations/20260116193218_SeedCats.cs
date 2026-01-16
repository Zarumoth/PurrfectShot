using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PurrfectShot.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedCats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Breed", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Tuxedo Cat", "Сладък, мил и добричък. Най-добрият котко-татко", "Сър Мортимър" },
                    { 2, "Европейска Късокосместа", "Най-сладката рижа маца. Най-лесно определена като котка с характер", "Лейди Фрайни" },
                    { 3, "Египетска Мау", "Отговаря на името си, най-бързият скокльо-котарак. Обича да води дълги и пълноценни разговори", "Венти" },
                    { 4, "Европейска Късокосместа", "Най-малкото ни вече не-бебе коте, модел Морти. Позната като Хъни-Бъни", "Хъни" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
