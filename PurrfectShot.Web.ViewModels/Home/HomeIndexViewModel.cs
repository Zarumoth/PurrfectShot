using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Web.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CatCardViewModel> FeaturedCats { get; set; }
            = new List<CatCardViewModel>();
        public int TotalPhotos { get; set; }
        public int TotalVotes { get; set; }
    }
}