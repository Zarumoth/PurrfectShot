namespace PurrfectShot.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using PurrfectShot.Web.Models;

    public class PurrfectShotDbContext : DbContext
    {
        public PurrfectShotDbContext(DbContextOptions<PurrfectShotDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public virtual DbSet<Cat> Cats { get; set; } = null!;

        public virtual DbSet<Photo> Photos { get; set; } = null!;

        public virtual DbSet<Vote> Votes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PurrfectShotDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
