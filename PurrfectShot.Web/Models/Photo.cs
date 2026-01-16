namespace PurrfectShot.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static PurrfectShot.Web.Common.EntityValidation.Photo;

    public class Photo
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateUploaded { get; set; }

        [Required]
        [MaxLength(CaptionMaxLength)]
        public string Caption { get; set; } = null!;

        [Required]
        [MaxLength(FilePathMaxLength)]
        public string FilePath { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Cat))]
        public int CatId { get; set; }

        public virtual Cat Cat { get; set; } = null!;

        public virtual ICollection<Vote> Votes { get; set; }
            = new HashSet<Vote>();
    }
}
