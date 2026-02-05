namespace PurrfectShot.Web.ViewModels.Photos
{
    public class PhotoCardViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = null!;
        public string CatName { get; set; } = null!;
        public DateTime DateUploaded { get; set; }
        public double Rating { get; set; }

    }
}