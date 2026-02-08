namespace PurrfectShot.Web.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static PurrfectShot.Web.Common.EntityValidation.Cat;

    [Comment("Represents a cat housemate in the system.")]
    public class Cat
    {
        [Key]
        [Comment("Unique identifier for the cat.")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("The name of the cat.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(BreedMaxLength)]
        [Comment("The specific breed of the cat.")]
        public string Breed { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [Comment("A short biography or description of the cat's personality.")]
        public string Description { get; set; } = null!;

        [Comment("Flag indicating if the cat is active or archived (soft-deleted).")]
        public bool IsActive { get; set; } = true;

        [Comment("The unique identifier of the photo chosen as the cat's profile picture.")]
        public Guid? MainPhotoId { get; set; }

        [Comment("Navigation property to the cat's primary profile picture.")]
        public virtual Photo? MainPhoto { get; set; }

        [Comment("Collection of all photos associated with this cat.")]
        public virtual ICollection<Photo> Photos { get; set; }
            = new HashSet<Photo>();
    }
}
