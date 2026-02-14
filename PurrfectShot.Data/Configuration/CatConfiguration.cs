using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Data.Models;

namespace PurrfectShot.Data.Configuration
{
    public class CatConfiguration : IEntityTypeConfiguration<Cat>
    {

        public void Configure(EntityTypeBuilder<Cat> builder)
        {
            builder
                .HasOne(c => c.MainPhoto)
                .WithMany()
                .HasForeignKey(c => c.MainPhotoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(c => c.Photos)
                .WithOne(p => p.Cat)
                .HasForeignKey(p => p.CatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(c => c.Owner)
                .WithMany(u => u.OwnedCats)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasData(CreateCats());
        }

        private List<Cat> CreateCats()
        {
            string adminId = "38058665-8726-41fa-be91-41de9acd0f72";
            var cats = new List<Cat>();

            cats.Add(new Cat
            {
                Id = 1,
                Name = "Сър Мортимър",
                Breed = "Tuxedo Cat",
                Description = "Негово Величество Сър Мортимър е не просто котарак, а стълбът на домашното спокойствие. Като истински джентълмен в смокинг, той е най-смелият пазител на семейните ценности и законен наследник на всички меки възглавници. Неговата суперсила е мъдростта, с която умиротворява всяка ситуация и надзирава реда в кралството.",
                IsActive = true,
                OwnerId = adminId
            });

            cats.Add(new Cat
            {
                Id = 2,
                Name = "Лейди Фрайни",
                Breed = "Европейска Късокосместа",
                Description = "Огнената дама на дома. Фрайни е перфектната комбинация от рижа сладост и желязна воля. Тя е върховният „граничен контрол“ и главен наложител на реда с лапа. Галенето е привилегия, която трябва да заслужите, а нейният характер е доказателство, че в това малко тяло живее истинска кралица.",
                IsActive = true,
                OwnerId = adminId

            });

            cats.Add(new Cat
            {
                Id = 3,
                Name = "Венти",
                Breed = "Египетска Мау",
                Description = "Венти е олицетворение на скоростта и енергията. С дух на древен египетски атлет и суперсили, придобити от радиоактивен тигър, той е най-бързият скокльо в семейството. Винаги готов за пълноценен разговор, той не просто мяука, а чуролика и споделя своите философски размисли за света.",
                IsActive = true,
                OwnerId = adminId
            });

            cats.Add(new Cat
            {
                Id = 4,
                Name = "Хъни-Бъни",
                Breed = "Европейска Късокосместа",
                Description = "Нашето малко бижу и „модел Морти 2.0“. Хъни-Бъни е неуморим изследовател на нови територии и скрити ъгълчета. Под зоркия поглед на своите трима ментори, тя ежедневно усвоява тайните на котешкото майсторство, превръщайки се в ултимативната комбинация от чар, игривост и приключения.",
                IsActive = true,
                OwnerId = adminId
            });

            return cats;
        }
    }
}
