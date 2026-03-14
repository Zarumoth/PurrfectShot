using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Data.Models;
using static PurrfectShot.Web.Common.EntityValidation.SeedConstants;

namespace PurrfectShot.Data.Configuration
{
    public class UserFavoritePhotoConfiguration : IEntityTypeConfiguration<UserFavoritePhoto>
    {
        public void Configure(EntityTypeBuilder<UserFavoritePhoto> builder)
        {
            builder.HasKey(uf => new { uf.UserId, uf.PhotoId });

            builder
                .HasOne(uf => uf.User)
                .WithMany(u => u.FavoritePhotos)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(uf => uf.Photo)
                .WithMany(p => p.UserFavoritePhotos)
                .HasForeignKey(uf => uf.PhotoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasData(GenerateFavorites());
        }

        private List<UserFavoritePhoto> GenerateFavorites()
        {
            var favorites = new List<UserFavoritePhoto>();

            favorites.Add(new UserFavoritePhoto { UserId = adminUserId, PhotoId = Guid.Parse("f86bfb53-eb39-4770-94f5-35a4710d037f") });
            favorites.Add(new UserFavoritePhoto { UserId = adminUserId, PhotoId = Guid.Parse("256e3cf7-f9ec-4f14-8361-e2e6fd2ce0e4") });
            favorites.Add(new UserFavoritePhoto { UserId = adminUserId, PhotoId = Guid.Parse("80377db9-ed3a-4013-b325-64651c2a4b6c") });
            favorites.Add(new UserFavoritePhoto { UserId = adminUserId, PhotoId = Guid.Parse("89bae15d-1c8e-4299-80ff-9c14578ab6ee") });
            favorites.Add(new UserFavoritePhoto { UserId = adminUserId, PhotoId = Guid.Parse("b79cb330-da37-4a09-8eaf-bfde8951051f") });

            return favorites;
        }
    }
}
