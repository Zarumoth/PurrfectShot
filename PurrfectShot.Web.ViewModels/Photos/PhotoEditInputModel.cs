using System.ComponentModel.DataAnnotations;
using static PurrfectShot.Web.Common.EntityValidation.Photo;

namespace PurrfectShot.Web.ViewModels.Photos
{
    public class PhotoEditInputModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int CatId { get; set; }

        [Required]
        public string CatName { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        public string PublisherId { get; set; } = null!;

        [Required(ErrorMessage = "Напиши нещо за снимката!")]
        [StringLength(CaptionMaxLength, MinimumLength = CaptionMinLength, ErrorMessage = "Описанието трябва да е между {2} и {1} символа.")]
        [Display(Name = "Описание на снимката")]
        public string Caption { get; set; } = null!;
    }
}