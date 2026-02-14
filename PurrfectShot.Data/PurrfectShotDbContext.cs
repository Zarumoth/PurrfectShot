namespace PurrfectShot.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using PurrfectShot.Data.Models;


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

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UsersClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UsersLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsersTokens");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PurrfectShotDbContext).Assembly);
        }
    }
}
