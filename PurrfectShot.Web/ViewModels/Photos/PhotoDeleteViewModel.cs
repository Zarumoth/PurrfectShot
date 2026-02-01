namespace PurrfectShot.Web.ViewModels.Photos
{
    public class PhotoDeleteViewModel
    {
        public int Id { get; set; }

        public int CatId { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string CatName { get; set; } = null!;
    }
}
