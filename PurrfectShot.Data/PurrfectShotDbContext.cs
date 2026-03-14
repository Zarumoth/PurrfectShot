namespace PurrfectShot.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PurrfectShot.Data.Models;
    using static PurrfectShot.Web.Common.EntityValidation.SeedConstants;


    public class PurrfectShotDbContext : IdentityDbContext<ApplicationUser>
    {
        public PurrfectShotDbContext(DbContextOptions<PurrfectShotDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cat> Cats { get; set; } = null!;

        public virtual DbSet<Photo> Photos { get; set; } = null!;

        public virtual DbSet<Vote> Votes { get; set; } = null!;

        public virtual DbSet<UserFavoritePhoto> UserFavoritePhotos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsersClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsersLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsersTokens");

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = adminRoleId
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = adminUserId,
                RoleId = adminRoleId
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PurrfectShotDbContext).Assembly);
        }
    }
}
