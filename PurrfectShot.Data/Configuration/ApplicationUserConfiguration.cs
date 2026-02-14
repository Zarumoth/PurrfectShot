using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Data.Models;

namespace PurrfectShot.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            //Admin User Id
            string adminId = "38058665-8726-41fa-be91-41de9acd0f72";

            var admin = new ApplicationUser
            {
                Id = adminId,
                UserName = "admin@purrfect.com",
                NormalizedUserName = "ADMIN@PURRFECT.COM",
                Email = "admin@purrfect.com",
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
