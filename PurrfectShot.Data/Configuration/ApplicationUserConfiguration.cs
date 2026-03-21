using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Data.Models;
using static PurrfectShot.Web.Common.EntityValidation.SeedConstants;

namespace PurrfectShot.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");


            var admin = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@purrfect.com",
                NormalizedUserName = "ADMIN@PURRFECT.COM",
                Email = adminEmail,
                NormalizedEmail = "ADMIN@PURRFECT.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            builder.HasData(admin);
        }
    }
}
