using System.ComponentModel.DataAnnotations;
using static PurrfectShot.Web.Common.EntityValidation.Cat;

namespace PurrfectShot.Web.ViewModels.Cats
{
    public class CatFormViewModel
    {
        [Required(ErrorMessage = "Всяко коте си има име!")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "Името трябва да е между {2} и {1} символа.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Породата е задължителна!")]
        [StringLength(BreedMaxLength, MinimumLength = BreedMinLength, ErrorMessage = "Породата трябва да е между {2} и {1} символа.")]
        public string Breed { get; set; } = null!;

        [Required(ErrorMessage = "Описанието е задължително!")]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = "Описанието трябва да е между {2} и {1} символа.")]
        public string Description { get; set; } = null!;
    }
}

