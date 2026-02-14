using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectShot.Data.Models
{
    [Comment("Mapping table for the Many-to-Many relationship between Users and their Favorite Photos.")]
    public class UserFavoritePhoto
    {
        [Required]
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public Guid PhotoId { get; set; }
        public virtual Photo Photo { get; set; } = null!;
    }
}
