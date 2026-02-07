using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Web.Models;

namespace PurrfectShot.Web.Data.Configuration
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
                .HasData(CreateCats());
        }

        private List<Cat> CreateCats()
        {
            var cats = new List<Cat>();

            cats.Add(new Cat
            {
                Id = 1,
                Name = "Сър Мортимър",
                Breed = "Tuxedo Cat",
                Description = "Сладък, мил и добричък. Най-добрият котко-татко",
                IsActive = true
            });

            cats.Add(new Cat
            {
                Id = 2,
                Name = "Лейди Фрайни",
                Breed = "Европейска Късокосместа",
                Description = "Най-сладката рижа маца. Най-лесно определена като котка с характер",
                IsActive = true

            });

            cats.Add(new Cat
            {
                Id = 3,
                Name = "Венти",
                Breed = "Египетска Мау",
                Description = "Отговаря на името си, най-бързият скокльо-котарак. Обича да води дълги и пълноценни разговори",
                IsActive = true
            });

            cats.Add(new Cat
            {
                Id = 4,
                Name = "Хъни-Бъни",
                Breed = "Европейска Късокосместа",
                Description = "Най-малкото ни вече не-бебе коте, модел Морти. Позната като Хъни-Бъни",
                IsActive = true
            });

            return cats;
        }
    }
}
