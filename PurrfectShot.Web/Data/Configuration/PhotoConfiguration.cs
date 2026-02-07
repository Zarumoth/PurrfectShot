using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Web.Models;

namespace PurrfectShot.Web.Data.Configuration
{
    public class PhotoConfiguration
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder
                .HasOne(p => p.Cat)
                .WithMany(c => c.Photos)
                .HasForeignKey(p => p.CatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.DateUploaded);
        }
    }
}
