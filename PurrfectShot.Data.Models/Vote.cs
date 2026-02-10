namespace PurrfectShot.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static PurrfectShot.Web.Common.EntityValidation.Vote;

    [Comment("Represents a single rating given to a photo.")]
    public class Vote
    {
        [Key]
        [Comment("Unique identifier for the vote entry.")]
        public int Id { get; set; }

        [Required]
        [Range(MinStarVoteValue, MaxStarVoteValue)]
        [Comment("The number of stars awarded (from 1 to 5).")]
        public int Stars { get; set; }

        [Required]
        [MaxLength(VoterNameMaxLength)]
        [Comment("The name of the user who cast the vote.")]
        public string VoterName { get; set; } = null!;

        [Required]
        [Comment("Foreign key referencing the rated photo.")]
        public Guid PhotoId { get; set; }

        [Comment("Navigation property to the photo being rated.")]
        public virtual Photo Photo { get; set; } = null!;
    }
}
