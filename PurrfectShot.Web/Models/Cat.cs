namespace PurrfectShot.Web.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static PurrfectShot.Web.Common.EntityValidation.Cat;

    public class Cat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(BreedMaxLength)]
        public string Breed { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("Indicates if the cat is still an active member of the household gallery.")]
        public bool IsActive { get; set; } = true;

        [Comment("The ID of the photo selected as the main profile picture for the cat.")]
        public int? MainPhotoId { get; set; }

        public virtual Photo? MainPhoto { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
            = new HashSet<Photo>();
    }
}