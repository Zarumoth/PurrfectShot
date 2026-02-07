using PurrfectShot.Web.ViewModels.Home;

namespace PurrfectShot.Web.ViewModels.Cats
{
    public class CatDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Breed { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        public IEnumerable<CatPhotoViewModel> Photos { get; set; } 
            = new List<CatPhotoViewModel>();

        public double OverallRating { get; set; }

    }
}
