using Microsoft.AspNetCore.Http;
using PurrfectShot.Web.ViewModels.Cats;
using System.ComponentModel.DataAnnotations;
using static PurrfectShot.Web.Common.EntityValidation.Photo;

namespace PurrfectShot.Web.ViewModels.Photos
{
    public class PhotoInputModel
    {
        [Required(ErrorMessage = "Няма избрана котка от списъка!")]
        [Display(Name = "Котка")]
        public int CatId { get; set; }

        [Required(ErrorMessage = "Липсва прикачен файл!")]
        [Display(Name = "Избери файл")]
        public IFormFile ImageFile { get; set; } = null!;

        [Required(ErrorMessage = "Напиши нещо за снимката!")]
        [StringLength(CaptionMaxLength, MinimumLength = CaptionMinLength)]
        [Display(Name = "Описание на снимката")]
        public string? Caption { get; set; }

        public IEnumerable<CatSelectViewModel>? Cats { get; set; }
    }
}
