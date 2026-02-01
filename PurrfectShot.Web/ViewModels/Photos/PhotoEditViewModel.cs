using System.ComponentModel.DataAnnotations;
using static PurrfectShot.Web.Common.EntityValidation.Photo;

namespace PurrfectShot.Web.ViewModels.Photos
{
    public class PhotoEditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string CatName { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Напиши нещо за снимката!")]
        [StringLength(CaptionMaxLength, MinimumLength = CaptionMinLength)]
        [Display(Name = "Описание на снимката")]
        public string Caption { get; set; } = null!;
    }
}