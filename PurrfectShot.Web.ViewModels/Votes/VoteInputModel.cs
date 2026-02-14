using System.ComponentModel.DataAnnotations;
using static PurrfectShot.Web.Common.EntityValidation.Vote;

namespace PurrfectShot.Web.ViewModels.Votes
{
    public class VoteInputModel
    {
        [Required]
        public Guid PhotoId { get; set; }

        [Required]
        [Range(MinStarVoteValue, MaxStarVoteValue)]
        public int Stars { get; set; }

        //Identity Implemented, data is taken from the user profile
        //[Required(ErrorMessage = "И хората си имат име, кажи си")]
        //[StringLength(VoterNameMaxLength, MinimumLength = VoterNameMinLength)]
        //public string VoterName { get; set; } = null!;
    }
}
