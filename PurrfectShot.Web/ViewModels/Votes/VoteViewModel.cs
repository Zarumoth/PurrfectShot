using System.ComponentModel.DataAnnotations;
using static PurrfectShot.Web.Common.EntityValidation.Vote;

namespace PurrfectShot.Web.ViewModels.Votes
{
    public class VoteViewModel
    {
        [Required]
        public int PhotoId { get; set; }

        [Required]
        [Range(MinStarVoteValue, MaxStarVoteValue)]
        public int Stars { get; set; }

        //TO-DO: When user authentication is added, pull VoterName from the user/account details
        [Required(ErrorMessage = "И хората си имат име, кажи си")]
        [StringLength(VoterNameMaxLength, MinimumLength = VoterNameMinLength)]
        public string VoterName { get; set; } = null!;
    }
}
