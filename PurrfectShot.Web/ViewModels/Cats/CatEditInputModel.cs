using System.ComponentModel.DataAnnotations;

namespace PurrfectShot.Web.ViewModels.Cats
{
    public class CatEditInputModel : CatInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}
