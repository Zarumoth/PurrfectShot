namespace PurrfectShot.Web.ViewModels.Photos
{
    public class PhotoDetailsViewModel
    {
        //Picture Info

        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public string UploadedOn { get; set; } = null!;


        //Cat Info

        public int CatId { get; set; }
        public string CatName { get; set; } = null!;
        public string CatBreed { get; set; } = null!;


        //Rating Info
        //TODO: Implement rating system

        public double Rating { get; set; }
        public int VotesCount { get; set; }
    }
}
