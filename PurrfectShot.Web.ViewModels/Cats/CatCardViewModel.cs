namespace PurrfectShot.Web.ViewModels.Cats
{
    public class CatCardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Breed { get; set; } = null!;
        public int PhotoCount { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}