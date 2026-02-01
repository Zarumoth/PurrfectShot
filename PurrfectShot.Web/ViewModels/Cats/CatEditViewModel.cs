using System.ComponentModel.DataAnnotations;

namespace PurrfectShot.Web.ViewModels.Cats
{
    public class CatEditViewModel : CatFormViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
