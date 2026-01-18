using System.ComponentModel.DataAnnotations;
using static PurrfectShot.Web.Common.EntityValidation.Cat;

namespace PurrfectShot.Web.ViewModels.Cats
{
    public class CatFormViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(BreedMaxLength, MinimumLength = BreedMinLength)]
        public string Breed { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;
    }
}

