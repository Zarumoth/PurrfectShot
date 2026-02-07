using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PurrfectShot.Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedPhotosData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "Caption", "CatId", "DateUploaded", "FilePath" },
                values: new object[,]
                {
                    { 101, "Ами ако никога не мърдна от тук?", 1, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_1.jpg" },
                    { 102, "Слийпи блем", 1, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_2.jpg" },
                    { 103, "Лордът на слънчевите бани", 1, new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_3.jpg" },
                    { 104, "Гледаме си от пенхауса", 1, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_4.jpg" },
                    { 105, "Заспал блем в котешкото легло", 1, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_5.jpg" },
                    { 106, "Отново... спим", 1, new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_6.jpg" },
                    { 107, "Морти и неговата кашоно-къща", 1, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_7.jpg" },
                    { 108, "Дай *прозявка* пет", 1, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_8.jpg" },
                    { 201, "Когато си най-сладката писанка на света", 2, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_1.jpg" },
                    { 202, "Размисли над живота от новото си легло", 2, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_2.jpg" },
                    { 203, "..или просто прозявка", 2, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_3.jpg" },
                    { 204, "В пастта на акулата", 2, new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_4.jpg" },
                    { 205, "Две котки, една перална", 2, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_5.jpg" },
                    { 206, "Не само щраусите могат така", 2, new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_6.jpg" },
                    { 207, "А ти какво гледаш?", 2, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_7.jpg" },
                    { 208, "Лейди Фрайни, върху трупът на лисицата", 2, new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_8.jpg" },
                    { 209, "Тунелни истории", 2, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_9.jpg" },
                    { 301, "Венти - най-бързият селфи-майстор", 3, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_1.jpg" },
                    { 302, "Ето така се мият котешките лапи", 3, new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_2.jpg" },
                    { 303, "Снейк кат", 3, new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_3.jpg" },
                    { 304, "Диванът не е достатъчно голям", 3, new DateTime(2026, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_4.jpg" },
                    { 305, "Vent do you want?", 3, new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_5.jpg" },
                    { 306, "Най-добре е върху лапата на папа", 3, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_6.jpg" },
                    { 307, "Малко блем и карти", 3, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_7.jpg" },
                    { 308, "Не, всъщност, ето така се мият котешки лапи", 3, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_8.jpg" },
                    { 309, "It's just me and my cat bed", 3, new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_9.jpg" },
                    { 310, "Венти, йога-котка", 3, new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_10.jpg" },
                    { 401, "Хъни-Бъни и нейната аура на сладост", 4, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_1.jpg" },
                    { 402, "Заспали и завити с на мама дрехите", 4, new DateTime(2026, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_2.jpg" },
                    { 403, "Когато се миеш на трона", 4, new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_3.jpg" },
                    { 404, "Дебнейки от стола", 4, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_4.jpg" },
                    { 405, "Honey Bunny means business", 4, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_5.jpg" },
                    { 406, "Облизваме се на стола на папа", 4, new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_6.jpg" },
                    { 407, "Папа, защо батко ме мие?", 4, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_7.jpg" },
                    { 408, "*музика от Цар Лъв*", 4, new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_8.jpg" },
                    { 409, "Знам, че той ме гледа", 4, new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_9.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_DateUploaded",
                table: "Photos",
                column: "DateUploaded");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Photos_DateUploaded",
                table: "Photos");

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 409);
        }
    }
}
