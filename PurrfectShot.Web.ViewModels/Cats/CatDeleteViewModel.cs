namespace PurrfectShot.Web.ViewModels.Cats
{
    public class CatDeleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool HasPhotos { get; set; }
        public string OwnerId { get; set; } = null!;
    }
}
