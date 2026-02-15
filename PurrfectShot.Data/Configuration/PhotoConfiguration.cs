using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Data.Models;

namespace PurrfectShot.Data.Configuration
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder
                .HasOne(p => p.Cat)
                .WithMany(c => c.Photos)
                .HasForeignKey(p => p.CatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.Publisher)
                .WithMany(c => c.UploadedPhotos)
                .HasForeignKey(p => p.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.DateUploaded);

            builder
                .HasData(AddPhotos());
        }

        private List<Photo> AddPhotos()
        {
            var photos = new List<Photo>();

            string adminId = "38058665-8726-41fa-be91-41de9acd0f72";

            // --- Group-1: Sir Mortimer (Id 1) ---
            photos.Add(new Photo { Id = Guid.Parse("f1085f28-5def-45a8-9f6b-64287e8c5413"), CatId = 1, Caption = "Ами ако никога не мърдна от тук?", FilePath = "/images/seed/SirMortiPhoto_1.jpg", DateUploaded = new DateTime(2026, 1, 2), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("7592715c-1d9a-4848-8c4d-2194fe0f477c"), CatId = 1, Caption = "Слийпи блем", FilePath = "/images/seed/SirMortiPhoto_2.jpg", DateUploaded = new DateTime(2026, 3, 14), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("bb536fa6-7323-42ac-99d3-971e1e9587ae"), CatId = 1, Caption = "Лордът на слънчевите бани", FilePath = "/images/seed/SirMortiPhoto_3.jpg", DateUploaded = new DateTime(2026, 1, 28), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("f86bfb53-eb39-4770-94f5-35a4710d037f"), CatId = 1, Caption = "Гледаме си от пенхауса", FilePath = "/images/seed/SirMortiPhoto_4.jpg", DateUploaded = new DateTime(2026, 4, 5), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("ffdb1b39-5ee0-471b-b666-f88e210ec99c"), CatId = 1, Caption = "Заспал блем в котешкото легло", FilePath = "/images/seed/SirMortiPhoto_5.jpg", DateUploaded = new DateTime(2026, 2, 14), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("7ed1f628-46ce-4fb8-9295-d93e976f0116"), CatId = 1, Caption = "Отново... спим", FilePath = "/images/seed/SirMortiPhoto_6.jpg", DateUploaded = new DateTime(2026, 7, 20), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("256e3cf7-f9ec-4f14-8361-e2e6fd2ce0e4"), CatId = 1, Caption = "Морти и неговата кашоно-къща", FilePath = "/images/seed/SirMortiPhoto_7.jpg", DateUploaded = new DateTime(2026, 1, 10), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("769bd5cb-e50b-46b5-875c-51ace4382828"), CatId = 1, Caption = "Дай *прозявка* пет", FilePath = "/images/seed/SirMortiPhoto_8.jpg", DateUploaded = new DateTime(2026, 6, 25), PublisherId = adminId });

            // --- Group-2: Lady Phryne (Id 2) ---
            photos.Add(new Photo { Id = Guid.Parse("40c09f56-f0c0-46e1-9c48-461458c3bbb0"), CatId = 2, Caption = "Когато си най-сладката писанка на света", FilePath = "/images/seed/LadyPhrynePhoto_1.jpg", DateUploaded = new DateTime(2026, 1, 5), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("4b7702ad-a3f7-4169-8e82-ebe4865b8953"), CatId = 2, Caption = "Размисли над живота от новото си легло", FilePath = "/images/seed/LadyPhrynePhoto_2.jpg", DateUploaded = new DateTime(2026, 2, 20), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("3f1d8d99-c8e4-4768-8f0c-caac648ad543"), CatId = 2, Caption = "..или просто прозявка", FilePath = "/images/seed/LadyPhrynePhoto_3.jpg", DateUploaded = new DateTime(2026, 2, 2), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("80377db9-ed3a-4013-b325-64651c2a4b6c"), CatId = 2, Caption = "В пастта на акулата", FilePath = "/images/seed/LadyPhrynePhoto_4.jpg", DateUploaded = new DateTime(2026, 6, 12), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("7db14ae0-d116-41ee-82db-a5d7abceee2a")  , CatId = 2, Caption = "Две котки, една перална", FilePath = "/images/seed/LadyPhrynePhoto_5.jpg", DateUploaded = new DateTime(2026, 7, 12), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("861ea7f6-af4e-4b67-96ff-61c6abe2fa0d")  , CatId = 2, Caption = "Не само щраусите могат така", FilePath = "/images/seed/LadyPhrynePhoto_6.jpg", DateUploaded = new DateTime(2026, 2, 8), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("864f6ad7-4eba-4848-83ed-a7eec7fe15e6")  , CatId = 2, Caption = "А ти какво гледаш?", FilePath = "/images/seed/LadyPhrynePhoto_7.jpg", DateUploaded = new DateTime(2026, 1, 30), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("2332efed-2870-427f-aefd-dbfd8bb14043"), CatId = 2, Caption = "Лейди Фрайни, върху трупът на лисицата", FilePath = "/images/seed/LadyPhrynePhoto_8.jpg", DateUploaded = new DateTime(2026, 2, 22), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("dbf3cd30-3b4b-483d-80ff-ef12a3eead94"), CatId = 2, Caption = "Тунелни истории", FilePath = "/images/seed/LadyPhrynePhoto_9.jpg", DateUploaded = new DateTime(2026, 8, 18), PublisherId = adminId });

            // --- Group-3: Venti (Id 3) ---
            photos.Add(new Photo { Id = Guid.Parse("42174d8b-9db8-4098-9f38-371005220780"), CatId = 3, Caption = "Венти - най-бързият селфи-майстор", FilePath = "/images/seed/VentiPhoto_1.jpg", DateUploaded = new DateTime(2026, 1, 3), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("89bae15d-1c8e-4299-80ff-9c14578ab6ee"), CatId = 3, Caption = "Ето така се мият котешките лапи", FilePath = "/images/seed/VentiPhoto_2.jpg", DateUploaded = new DateTime(2026, 2, 17), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("b3dff3d3-0115-4ac5-9cd9-192c5f059109"), CatId = 3, Caption = "Снейк кат", FilePath = "/images/seed/VentiPhoto_3.jpg", DateUploaded = new DateTime(2026, 10, 31), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("0aca87fc-58b9-483b-b80e-74a811811372"), CatId = 3, Caption = "Диванът не е достатъчно голям", FilePath = "/images/seed/VentiPhoto_4.jpg", DateUploaded = new DateTime(2026, 2, 7), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("3360a037-aa1d-4bf5-b04b-da6afac92599"), CatId = 3, Caption = "Vent do you want?", FilePath = "/images/seed/VentiPhoto_5.jpg", DateUploaded = new DateTime(2026, 12, 19), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("be283f19-0b3c-4adb-b235-e4b64d247db4"), CatId = 3, Caption = "Най-добре е върху лапата на папа", FilePath = "/images/seed/VentiPhoto_6.jpg", DateUploaded = new DateTime(2026, 1, 8), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("ac07d5d6-c099-45e9-b725-f09ad314e9d3"), CatId = 3, Caption = "Малко блем и карти", FilePath = "/images/seed/VentiPhoto_7.jpg", DateUploaded = new DateTime(2026, 2, 3), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("38058665-8726-41fa-be91-41de9acd0f72"), CatId = 3, Caption = "Не, всъщност, ето така се мият котешки лапи", FilePath = "/images/seed/VentiPhoto_8.jpg", DateUploaded = new DateTime(2026, 1, 22), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("7f2e351c-6c8e-4261-ae66-b29bded5298d"), CatId = 3, Caption = "It's just me and my cat bed", FilePath = "/images/seed/VentiPhoto_9.jpg", DateUploaded = new DateTime(2026, 2, 15), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("5e14f8a8-6bd3-41d1-aca1-0d8f052d9241"), CatId = 3, Caption = "Венти, йога-котка", FilePath = "/images/seed/VentiPhoto_10.jpg", DateUploaded = new DateTime(2026, 11, 28), PublisherId = adminId });

            // --- Group-4: Honey (Id 4) ---
            photos.Add(new Photo { Id = Guid.Parse("9940e6f5-8edd-4e94-ad94-89579118a578"), CatId = 4, Caption = "Хъни-Бъни и нейната аура на сладост", FilePath = "/images/seed/HoneyBuneyPhoto_1.jpg", DateUploaded = new DateTime(2026, 11, 6), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("b79cb330-da37-4a09-8eaf-bfde8951051f"), CatId = 4, Caption = "Заспали и завити с на мама дрехите", FilePath = "/images/seed/HoneyBuneyPhoto_2.jpg", DateUploaded = new DateTime(2026, 11, 24), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("f62689fd-0798-4eb7-a0de-d9dc98f9b1fd"), CatId = 4, Caption = "Когато се миеш на трона", FilePath = "/images/seed/HoneyBuneyPhoto_3.jpg", DateUploaded = new DateTime(2026, 12, 4), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("dcf0db9d-886f-40b1-9078-09cb80973b3a"), CatId = 4, Caption = "Дебнейки от стола", FilePath = "/images/seed/HoneyBuneyPhoto_4.jpg", DateUploaded = new DateTime(2026, 2, 11), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("0c74aadc-266b-47cd-9c56-995b73324753"), CatId = 4, Caption = "Honey Bunny means business", FilePath = "/images/seed/HoneyBuneyPhoto_5.jpg", DateUploaded = new DateTime(2026, 2, 18), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("fb6ca040-2d9c-4793-9d75-17fb6cca6ae4"), CatId = 4, Caption = "Облизваме се на стола на папа", FilePath = "/images/seed/HoneyBuneyPhoto_6.jpg", DateUploaded = new DateTime(2026, 3, 13), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("59392be0-f7de-4bcd-84b2-99f19a341503"), CatId = 4, Caption = "Папа, защо батко ме мие?", FilePath = "/images/seed/HoneyBuneyPhoto_7.jpg", DateUploaded = new DateTime(2026, 2, 26), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("3c164cdb-459f-4c9b-947d-60dc957085fe"), CatId = 4, Caption = "*музика от Цар Лъв*", FilePath = "/images/seed/HoneyBuneyPhoto_8.jpg", DateUploaded = new DateTime(2026, 7, 27), PublisherId = adminId });
            photos.Add(new Photo { Id = Guid.Parse("0b0ede57-0b57-4ba2-abac-bb9468aca00c"), CatId = 4, Caption = "Знам, че той ме гледа", FilePath = "/images/seed/HoneyBuneyPhoto_9.jpg", DateUploaded = new DateTime(2026, 2, 9), PublisherId = adminId });

            return photos;
        }
    }
}