using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PurrfectShot.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialFullSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UsersLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UsersTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the cat.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the cat."),
                    Breed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The specific breed of the cat."),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "A short biography or description of the cat's personality."),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Flag indicating if the cat is active or archived (soft-deleted)."),
                    MainPhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "The unique identifier of the photo chosen as the cat's profile picture."),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true, comment: "The ID of the user who is the owner/publisher of this cat profile.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cats_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Represents a cat housemate in the system.");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Unique identifier for the photo entry."),
                    DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The exact date and time when the photo was uploaded."),
                    Caption = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "A descriptive text or story accompanying the photo."),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The relative server path where the physical image file is stored."),
                    CatId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key referencing the cat shown in the photo.")
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
                },
                comment: "Represents an uploaded image of a cat.");

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the vote entry.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stars = table.Column<int>(type: "int", nullable: false, comment: "The number of stars awarded (from 1 to 5)."),
                    VoterName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "The name of the user who cast the vote."),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key referencing the person who voted photo."),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Foreign key referencing the rated photo.")
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
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represents a single rating given to a photo.");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "38058665-8726-41fa-be91-41de9acd0f72", 0, "2732508c-2754-4372-8c1a-74bf753ce019", "admin@purrfect.com", true, false, null, "ADMIN@PURRFECT.COM", "ADMIN@PURRFECT.COM", "AQAAAAIAAYagAAAAEKZhyatLgzuyt99I6A4UXRwlS2mnxTGwmt9xSQtyt8bAenSyVu9i5+d+ZjZK83jDRA==", null, false, "d656430a-27ae-485e-9c2b-bd230d89c5c3", false, "admin@purrfect.com" });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Breed", "Description", "IsActive", "MainPhotoId", "Name", "OwnerId" },
                values: new object[,]
                {
                    { 1, "Tuxedo Cat", "Негово Величество Сър Мортимър е не просто котарак, а стълбът на домашното спокойствие. Като истински джентълмен в смокинг, той е най-смелият пазител на семейните ценности и законен наследник на всички меки възглавници. Неговата суперсила е мъдростта, с която умиротворява всяка ситуация и надзирава реда в кралството.", true, null, "Сър Мортимър", "38058665-8726-41fa-be91-41de9acd0f72" },
                    { 2, "Европейска Късокосместа", "Огнената дама на дома. Фрайни е перфектната комбинация от рижа сладост и желязна воля. Тя е върховният „граничен контрол“ и главен наложител на реда с лапа. Галенето е привилегия, която трябва да заслужите, а нейният характер е доказателство, че в това малко тяло живее истинска кралица.", true, null, "Лейди Фрайни", "38058665-8726-41fa-be91-41de9acd0f72" },
                    { 3, "Египетска Мау", "Венти е олицетворение на скоростта и енергията. С дух на древен египетски атлет и суперсили, придобити от радиоактивен тигър, той е най-бързият скокльо в семейството. Винаги готов за пълноценен разговор, той не просто мяука, а чуролика и споделя своите философски размисли за света.", true, null, "Венти", "38058665-8726-41fa-be91-41de9acd0f72" },
                    { 4, "Европейска Късокосместа", "Нашето малко бижу и „модел Морти 2.0“. Хъни-Бъни е неуморим изследовател на нови територии и скрити ъгълчета. Под зоркия поглед на своите трима ментори, тя ежедневно усвоява тайните на котешкото майсторство, превръщайки се в ултимативната комбинация от чар, игривост и приключения.", true, null, "Хъни-Бъни", "38058665-8726-41fa-be91-41de9acd0f72" }
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

            migrationBuilder.InsertData(
                table: "Votes",
                columns: new[] { "Id", "PhotoId", "Stars", "UserId", "VoterName" },
                values: new object[,]
                {
                    { 1000, new Guid("f1085f28-5def-45a8-9f6b-64287e8c5413"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Птеротатко" },
                    { 1001, new Guid("f1085f28-5def-45a8-9f6b-64287e8c5413"), 4, "38058665-8726-41fa-be91-41de9acd0f72", "Трицерабобс" },
                    { 1002, new Guid("7592715c-1d9a-4848-8c4d-2194fe0f477c"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Термаминатор" },
                    { 1003, new Guid("bb536fa6-7323-42ac-99d3-971e1e9587ae"), 3, "38058665-8726-41fa-be91-41de9acd0f72", "Птеротатко" },
                    { 1004, new Guid("40c09f56-f0c0-46e1-9c48-461458c3bbb0"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Трицерабобс" },
                    { 1005, new Guid("40c09f56-f0c0-46e1-9c48-461458c3bbb0"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Птеротатко" },
                    { 1006, new Guid("7db14ae0-d116-41ee-82db-a5d7abceee2a"), 4, "38058665-8726-41fa-be91-41de9acd0f72", "Термаминатор" },
                    { 1007, new Guid("42174d8b-9db8-4098-9f38-371005220780"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Трицерабобс" },
                    { 1008, new Guid("38058665-8726-41fa-be91-41de9acd0f72"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Птеротатко" },
                    { 1009, new Guid("38058665-8726-41fa-be91-41de9acd0f72"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Термаминатор" },
                    { 1010, new Guid("9940e6f5-8edd-4e94-ad94-89579118a578"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Термаминатор" },
                    { 1011, new Guid("0b0ede57-0b57-4ba2-abac-bb9468aca00c"), 4, "38058665-8726-41fa-be91-41de9acd0f72", "Трицерабобс" },
                    { 1012, new Guid("0b0ede57-0b57-4ba2-abac-bb9468aca00c"), 5, "38058665-8726-41fa-be91-41de9acd0f72", "Птеротатко" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cats_MainPhotoId",
                table: "Cats",
                column: "MainPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_OwnerId",
                table: "Cats",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CatId",
                table: "Photos",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_DateUploaded",
                table: "Photos",
                column: "DateUploaded");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RolesClaims_RoleId",
                table: "RolesClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsersClaims_UserId",
                table: "UsersClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersLogins_UserId",
                table: "UsersLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PhotoId",
                table: "Votes",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");

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
                name: "RolesClaims");

            migrationBuilder.DropTable(
                name: "UsersClaims");

            migrationBuilder.DropTable(
                name: "UsersLogins");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "UsersTokens");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
