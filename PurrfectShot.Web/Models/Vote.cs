namespace PurrfectShot.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static PurrfectShot.Web.Common.EntityValidation.Vote;

    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(MinStarVoteValue, MaxStarVoteValue)]
        public int Stars { get; set; }

        [Required]
        [MaxLength(VoterNameMaxLength)]
        public string VoterName { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; } = null!;
    }
}
