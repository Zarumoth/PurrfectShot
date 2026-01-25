namespace PurrfectShot.Web.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<CatCardViewModel> FeaturedCats { get; set; }
            = new List<CatCardViewModel>();
        public int TotalPhotos { get; set; }
        public int TotalVotes { get; set; }
    }

    public class CatCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Breed { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
    }
}