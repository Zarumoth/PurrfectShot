using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Web.Models;

namespace PurrfectShot.Web.Data.Configuration

{
    public class VoteConfiguration
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder
                .HasOne(v => v.Photo)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PhotoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
