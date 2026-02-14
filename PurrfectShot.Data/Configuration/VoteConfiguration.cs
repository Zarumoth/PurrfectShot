using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurrfectShot.Data.Models;

namespace PurrfectShot.Data.Configuration

{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder
                .HasOne(v => v.Photo)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PhotoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasData(GenerateVotes());
        }

        private List<Vote> GenerateVotes()
        {
            var votes = new List<Vote>();
            int idCounter = 1000;

            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("f1085f28-5def-45a8-9f6b-64287e8c5413"), VoterName = "Птеротатко", Stars = 5 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("f1085f28-5def-45a8-9f6b-64287e8c5413"), VoterName = "Трицерабобс", Stars = 4 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("7592715c-1d9a-4848-8c4d-2194fe0f477c"), VoterName = "Термаминатор", Stars = 5 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("bb536fa6-7323-42ac-99d3-971e1e9587ae"), VoterName = "Птеротатко", Stars = 3 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("40c09f56-f0c0-46e1-9c48-461458c3bbb0"), VoterName = "Трицерабобс", Stars = 5 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("40c09f56-f0c0-46e1-9c48-461458c3bbb0"), VoterName = "Птеротатко", Stars = 5 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("7db14ae0-d116-41ee-82db-a5d7abceee2a"), VoterName = "Термаминатор", Stars = 4 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("42174d8b-9db8-4098-9f38-371005220780"), VoterName = "Трицерабобс", Stars = 5 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("38058665-8726-41fa-be91-41de9acd0f72"), VoterName = "Птеротатко", Stars = 5 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("38058665-8726-41fa-be91-41de9acd0f72"), VoterName = "Термаминатор", Stars = 5 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("9940e6f5-8edd-4e94-ad94-89579118a578"), VoterName = "Термаминатор", Stars = 5 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("0b0ede57-0b57-4ba2-abac-bb9468aca00c"), VoterName = "Трицерабобс", Stars = 4 });
            votes.Add(new Vote { Id = idCounter++, PhotoId = Guid.Parse("0b0ede57-0b57-4ba2-abac-bb9468aca00c"), VoterName = "Птеротатко", Stars = 5 });

            return votes;
        }
    }
}
