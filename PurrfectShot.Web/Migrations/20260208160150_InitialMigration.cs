using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PurrfectShot.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates if the cat is still an active member of the household gallery."),
                    MainPhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "The ID of the photo selected as the main profile picture for the cat.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    VoterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Breed", "Description", "IsActive", "MainPhotoId", "Name" },
                values: new object[,]
                {
                    { 1, "Tuxedo Cat", "Сладък, мил и добричък. Най-добрият котко-татко", true, null, "Сър Мортимър" },
                    { 2, "Европейска Късокосместа", "Най-сладката рижа маца. Най-лесно определена като котка с характер", true, null, "Лейди Фрайни" },
                    { 3, "Египетска Мау", "Отговаря на името си, най-бързият скокльо-котарак. Обича да води дълги и пълноценни разговори", true, null, "Венти" },
                    { 4, "Европейска Късокосместа", "Най-малкото ни вече не-бебе коте, модел Морти. Позната като Хъни-Бъни", true, null, "Хъни-Бъни" }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "Caption", "CatId", "DateUploaded", "FilePath" },
                values: new object[,]
                {
                    { new Guid("0aca87fc-58b9-483b-b80e-74a811811372"), "Диванът не е достатъчно голям", 3, new DateTime(2026, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_4.jpg" },
                    { new Guid("0b0ede57-0b57-4ba2-abac-bb9468aca00c"), "Знам, че той ме гледа", 4, new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_9.jpg" },
                    { new Guid("0c74aadc-266b-47cd-9c56-995b73324753"), "Honey Bunny means business", 4, new DateTime(2026, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_5.jpg" },
                    { new Guid("2332efed-2870-427f-aefd-dbfd8bb14043"), "Лейди Фрайни, върху трупът на лисицата", 2, new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_8.jpg" },
                    { new Guid("256e3cf7-f9ec-4f14-8361-e2e6fd2ce0e4"), "Морти и неговата кашоно-къща", 1, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_7.jpg" },
                    { new Guid("3360a037-aa1d-4bf5-b04b-da6afac92599"), "Vent do you want?", 3, new DateTime(2026, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_5.jpg" },
                    { new Guid("38058665-8726-41fa-be91-41de9acd0f72"), "Не, всъщност, ето така се мият котешки лапи", 3, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_8.jpg" },
                    { new Guid("3c164cdb-459f-4c9b-947d-60dc957085fe"), "*музика от Цар Лъв*", 4, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_8.jpg" },
                    { new Guid("3f1d8d99-c8e4-4768-8f0c-caac648ad543"), "..или просто прозявка", 2, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_3.jpg" },
                    { new Guid("40c09f56-f0c0-46e1-9c48-461458c3bbb0"), "Когато си най-сладката писанка на света", 2, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_1.jpg" },
                    { new Guid("42174d8b-9db8-4098-9f38-371005220780"), "Венти - най-бързият селфи-майстор", 3, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_1.jpg" },
                    { new Guid("4b7702ad-a3f7-4169-8e82-ebe4865b8953"), "Размисли над живота от новото си легло", 2, new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_2.jpg" },
                    { new Guid("59392be0-f7de-4bcd-84b2-99f19a341503"), "Папа, защо батко ме мие?", 4, new DateTime(2026, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_7.jpg" },
                    { new Guid("5e14f8a8-6bd3-41d1-aca1-0d8f052d9241"), "Венти, йога-котка", 3, new DateTime(2026, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_10.jpg" },
                    { new Guid("7592715c-1d9a-4848-8c4d-2194fe0f477c"), "Слийпи блем", 1, new DateTime(2026, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_2.jpg" },
                    { new Guid("769bd5cb-e50b-46b5-875c-51ace4382828"), "Дай *прозявка* пет", 1, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_8.jpg" },
                    { new Guid("7db14ae0-d116-41ee-82db-a5d7abceee2a"), "Две котки, една перална", 2, new DateTime(2026, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_5.jpg" },
                    { new Guid("7ed1f628-46ce-4fb8-9295-d93e976f0116"), "Отново... спим", 1, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_6.jpg" },
                    { new Guid("7f2e351c-6c8e-4261-ae66-b29bded5298d"), "It's just me and my cat bed", 3, new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_9.jpg" },
                    { new Guid("80377db9-ed3a-4013-b325-64651c2a4b6c"), "В пастта на акулата", 2, new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_4.jpg" },
                    { new Guid("861ea7f6-af4e-4b67-96ff-61c6abe2fa0d"), "Не само щраусите могат така", 2, new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_6.jpg" },
                    { new Guid("864f6ad7-4eba-4848-83ed-a7eec7fe15e6"), "А ти какво гледаш?", 2, new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_7.jpg" },
                    { new Guid("89bae15d-1c8e-4299-80ff-9c14578ab6ee"), "Ето така се мият котешките лапи", 3, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_2.jpg" },
                    { new Guid("9940e6f5-8edd-4e94-ad94-89579118a578"), "Хъни-Бъни и нейната аура на сладост", 4, new DateTime(2026, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_1.jpg" },
                    { new Guid("ac07d5d6-c099-45e9-b725-f09ad314e9d3"), "Малко блем и карти", 3, new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_7.jpg" },
                    { new Guid("b3dff3d3-0115-4ac5-9cd9-192c5f059109"), "Снейк кат", 3, new DateTime(2026, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_3.jpg" },
                    { new Guid("b79cb330-da37-4a09-8eaf-bfde8951051f"), "Заспали и завити с на мама дрехите", 4, new DateTime(2026, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_2.jpg" },
                    { new Guid("bb536fa6-7323-42ac-99d3-971e1e9587ae"), "Лордът на слънчевите бани", 1, new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_3.jpg" },
                    { new Guid("be283f19-0b3c-4adb-b235-e4b64d247db4"), "Най-добре е върху лапата на папа", 3, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/VentiPhoto_6.jpg" },
                    { new Guid("dbf3cd30-3b4b-483d-80ff-ef12a3eead94"), "Тунелни истории", 2, new DateTime(2026, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/LadyPhrynePhoto_9.jpg" },
                    { new Guid("dcf0db9d-886f-40b1-9078-09cb80973b3a"), "Дебнейки от стола", 4, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_4.jpg" },
                    { new Guid("f1085f28-5def-45a8-9f6b-64287e8c5413"), "Ами ако никога не мърдна от тук?", 1, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_1.jpg" },
                    { new Guid("f62689fd-0798-4eb7-a0de-d9dc98f9b1fd"), "Когато се миеш на трона", 4, new DateTime(2026, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_3.jpg" },
                    { new Guid("f86bfb53-eb39-4770-94f5-35a4710d037f"), "Гледаме си от пенхауса", 1, new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_4.jpg" },
                    { new Guid("fb6ca040-2d9c-4793-9d75-17fb6cca6ae4"), "Облизваме се на стола на папа", 4, new DateTime(2026, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/HoneyBuneyPhoto_6.jpg" },
                    { new Guid("ffdb1b39-5ee0-471b-b666-f88e210ec99c"), "Заспал блем в котешкото легло", 1, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "/images/seed/SirMortiPhoto_5.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cats_MainPhotoId",
                table: "Cats",
                column: "MainPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CatId",
                table: "Photos",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_DateUploaded",
                table: "Photos",
                column: "DateUploaded");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PhotoId",
                table: "Votes",
                column: "PhotoId");

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

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Cats");
        }
    }
}
