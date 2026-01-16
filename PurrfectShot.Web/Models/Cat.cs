namespace PurrfectShot.Web.Models
{
    using System.ComponentModel.DataAnnotations;
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

        public virtual ICollection<Photo> Photos { get; set; }
            = new HashSet<Photo>();
    }
}