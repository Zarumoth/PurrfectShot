using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Web.Models;

namespace PurrfectShot.Web.Data.Configuration
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

            builder.HasIndex(p => p.DateUploaded);

            builder
                .HasData(AddPhotos());
        }

        private List<Photo> AddPhotos()
        {
            var photos = new List<Photo>();

            // --- Group-1: Sir Mortimer (Id 1) ---
            photos.Add(new Photo { Id = 101, CatId = 1, Caption = "Ами ако никога не мърдна от тук?", FilePath = "/images/seed/SirMortiPhoto_1.jpg", DateUploaded = new DateTime(2026, 1, 2) });
            photos.Add(new Photo { Id = 102, CatId = 1, Caption = "Слийпи блем", FilePath = "/images/seed/SirMortiPhoto_2.jpg", DateUploaded = new DateTime(2026, 3, 14) });
            photos.Add(new Photo { Id = 103, CatId = 1, Caption = "Лордът на слънчевите бани", FilePath = "/images/seed/SirMortiPhoto_3.jpg", DateUploaded = new DateTime(2026, 1, 28) });
            photos.Add(new Photo { Id = 104, CatId = 1, Caption = "Гледаме си от пенхауса", FilePath = "/images/seed/SirMortiPhoto_4.jpg", DateUploaded = new DateTime(2026, 4, 5) });
            photos.Add(new Photo { Id = 105, CatId = 1, Caption = "Заспал блем в котешкото легло", FilePath = "/images/seed/SirMortiPhoto_5.jpg", DateUploaded = new DateTime(2026, 2, 14) });
            photos.Add(new Photo { Id = 106, CatId = 1, Caption = "Отново... спим", FilePath = "/images/seed/SirMortiPhoto_6.jpg", DateUploaded = new DateTime(2026, 7, 20) });
            photos.Add(new Photo { Id = 107, CatId = 1, Caption = "Морти и неговата кашоно-къща", FilePath = "/images/seed/SirMortiPhoto_7.jpg", DateUploaded = new DateTime(2026, 1, 10) });
            photos.Add(new Photo { Id = 108, CatId = 1, Caption = "Дай *прозявка* пет", FilePath = "/images/seed/SirMortiPhoto_8.jpg", DateUploaded = new DateTime(2026, 6, 25) });

            // --- Group-2: Lady Phryne (Id 2) ---
            photos.Add(new Photo { Id = 201, CatId = 2, Caption = "Когато си най-сладката писанка на света", FilePath = "/images/seed/LadyPhrynePhoto_1.jpg", DateUploaded = new DateTime(2026, 1, 5) });
            photos.Add(new Photo { Id = 202, CatId = 2, Caption = "Размисли над живота от новото си легло", FilePath = "/images/seed/LadyPhrynePhoto_2.jpg", DateUploaded = new DateTime(2026, 2, 20) });
            photos.Add(new Photo { Id = 203, CatId = 2, Caption = "..или просто прозявка", FilePath = "/images/seed/LadyPhrynePhoto_3.jpg", DateUploaded = new DateTime(2026, 2, 2) });
            photos.Add(new Photo { Id = 204, CatId = 2, Caption = "В пастта на акулата", FilePath = "/images/seed/LadyPhrynePhoto_4.jpg", DateUploaded = new DateTime(2026, 6, 12) });
            photos.Add(new Photo { Id = 205, CatId = 2, Caption = "Две котки, една перална", FilePath = "/images/seed/LadyPhrynePhoto_5.jpg", DateUploaded = new DateTime(2026, 7, 12) });
            photos.Add(new Photo { Id = 206, CatId = 2, Caption = "Не само щраусите могат така", FilePath = "/images/seed/LadyPhrynePhoto_6.jpg", DateUploaded = new DateTime(2026, 2, 8) });
            photos.Add(new Photo { Id = 207, CatId = 2, Caption = "А ти какво гледаш?", FilePath = "/images/seed/LadyPhrynePhoto_7.jpg", DateUploaded = new DateTime(2026, 1, 30) });
            photos.Add(new Photo { Id = 208, CatId = 2, Caption = "Лейди Фрайни, върху трупът на лисицата", FilePath = "/images/seed/LadyPhrynePhoto_8.jpg", DateUploaded = new DateTime(2026, 2, 22) });
            photos.Add(new Photo { Id = 209, CatId = 2, Caption = "Тунелни истории", FilePath = "/images/seed/LadyPhrynePhoto_9.jpg", DateUploaded = new DateTime(2026, 8, 18) });

            // --- Group-3: Venti (Id 3) ---
            photos.Add(new Photo { Id = 301, CatId = 3, Caption = "Венти - най-бързият селфи-майстор", FilePath = "/images/seed/VentiPhoto_1.jpg", DateUploaded = new DateTime(2026, 1, 3) });
            photos.Add(new Photo { Id = 302, CatId = 3, Caption = "Ето така се мият котешките лапи", FilePath = "/images/seed/VentiPhoto_2.jpg", DateUploaded = new DateTime(2026, 2, 17) });
            photos.Add(new Photo { Id = 303, CatId = 3, Caption = "Снейк кат", FilePath = "/images/seed/VentiPhoto_3.jpg", DateUploaded = new DateTime(2026, 10, 31) });
            photos.Add(new Photo { Id = 304, CatId = 3, Caption = "Диванът не е достатъчно голям", FilePath = "/images/seed/VentiPhoto_4.jpg", DateUploaded = new DateTime(2026, 2, 7) });
            photos.Add(new Photo { Id = 305, CatId = 3, Caption = "Vent do you want?", FilePath = "/images/seed/VentiPhoto_5.jpg", DateUploaded = new DateTime(2026, 12, 19) });
            photos.Add(new Photo { Id = 306, CatId = 3, Caption = "Най-добре е върху лапата на папа", FilePath = "/images/seed/VentiPhoto_6.jpg", DateUploaded = new DateTime(2026, 1, 8) });
            photos.Add(new Photo { Id = 307, CatId = 3, Caption = "Малко блем и карти", FilePath = "/images/seed/VentiPhoto_7.jpg", DateUploaded = new DateTime(2026, 2, 3) });
            photos.Add(new Photo { Id = 308, CatId = 3, Caption = "Не, всъщност, ето така се мият котешки лапи", FilePath = "/images/seed/VentiPhoto_8.jpg", DateUploaded = new DateTime(2026, 1, 22) });
            photos.Add(new Photo { Id = 309, CatId = 3, Caption = "It's just me and my cat bed", FilePath = "/images/seed/VentiPhoto_9.jpg", DateUploaded = new DateTime(2026, 2, 15) });
            photos.Add(new Photo { Id = 310, CatId = 3, Caption = "Венти, йога-котка", FilePath = "/images/seed/VentiPhoto_10.jpg", DateUploaded = new DateTime(2026, 11, 28) });

            // --- Group-4: Honey (Id 4) ---
            photos.Add(new Photo { Id = 401, CatId = 4, Caption = "Хъни-Бъни и нейната аура на сладост", FilePath = "/images/seed/HoneyBuneyPhoto_1.jpg", DateUploaded = new DateTime(2026, 11, 6) });
            photos.Add(new Photo { Id = 402, CatId = 4, Caption = "Заспали и завити с на мама дрехите", FilePath = "/images/seed/HoneyBuneyPhoto_2.jpg", DateUploaded = new DateTime(2026, 11, 24) });
            photos.Add(new Photo { Id = 403, CatId = 4, Caption = "Когато се миеш на трона", FilePath = "/images/seed/HoneyBuneyPhoto_3.jpg", DateUploaded = new DateTime(2026, 12, 4) });
            photos.Add(new Photo { Id = 404, CatId = 4, Caption = "Дебнейки от стола", FilePath = "/images/seed/HoneyBuneyPhoto_4.jpg", DateUploaded = new DateTime(2026, 2, 11) });
            photos.Add(new Photo { Id = 405, CatId = 4, Caption = "Honey Bunny means business", FilePath = "/images/seed/HoneyBuneyPhoto_5.jpg", DateUploaded = new DateTime(2026, 2, 18) });
            photos.Add(new Photo { Id = 406, CatId = 4, Caption = "Облизваме се на стола на папа", FilePath = "/images/seed/HoneyBuneyPhoto_6.jpg", DateUploaded = new DateTime(2026, 3, 13) });
            photos.Add(new Photo { Id = 407, CatId = 4, Caption = "Папа, защо батко ме мие?", FilePath = "/images/seed/HoneyBuneyPhoto_7.jpg", DateUploaded = new DateTime(2026, 2, 26) });
            photos.Add(new Photo { Id = 408, CatId = 4, Caption = "*музика от Цар Лъв*", FilePath = "/images/seed/HoneyBuneyPhoto_8.jpg", DateUploaded = new DateTime(2026, 7, 27) });
            photos.Add(new Photo { Id = 409, CatId = 4, Caption = "Знам, че той ме гледа", FilePath = "/images/seed/HoneyBuneyPhoto_9.jpg", DateUploaded = new DateTime(2026, 2, 9) });

            return photos;
        }
    }
}