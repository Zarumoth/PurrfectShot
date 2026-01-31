namespace PurrfectShot.Web.ViewModels.Cats
{
    public class CatPhotoViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public double Rating { get; set; }

        public DateTime DateUploaded { get; set; }
    }
}
