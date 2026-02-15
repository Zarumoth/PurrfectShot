namespace PurrfectShot.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static PurrfectShot.Web.Common.EntityValidation.Photo;

    [Comment("Represents an uploaded image of a cat.")]
    public class Photo
    {
        [Key]
        [Comment("Unique identifier for the photo entry.")]
        public Guid Id { get; set; }

        [Comment("The exact date and time when the photo was uploaded.")]
        public DateTime DateUploaded { get; set; }

        [Required]
        [MaxLength(CaptionMaxLength)]
        [Comment("A descriptive text or story accompanying the photo.")]
        public string Caption { get; set; } = null!;

        [Required]
        [MaxLength(FilePathMaxLength)]
        [Comment("The relative server path where the physical image file is stored.")]
        public string FilePath { get; set; } = null!;

        [Required]
        [Comment("Foreign key referencing the photo uploader.")]
        public string PublisherId { get; set; } = null!;

        [Comment("Navigation property to the user who uploaded the photo.")]
        public virtual ApplicationUser Publisher { get; set; } = null!;

        [Required]
        [Comment("Foreign key referencing the cat shown in the photo.")]
        public int CatId { get; set; }

        [Comment("Navigation property to the cat that owns this photo.")]
        public virtual Cat Cat { get; set; } = null!;

        [Comment("Collection of star ratings received for this specific photo.")]
        public virtual ICollection<Vote> Votes { get; set; }
            = new HashSet<Vote>();

        [Comment("Collection of user favorites that include this photo.")]
        public virtual ICollection<UserFavoritePhoto> UserFavoritePhotos { get; set; }
            = new HashSet<UserFavoritePhoto>();
    }
}
