using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PurrfectShot.Data.Models
{
    [Comment("Represents the signed into the application user and their attributes")]
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Cat> OwnedCats { get; set; } 
            = new HashSet<Cat>();

        public virtual ICollection<Vote> Votes { get; set; }
            = new HashSet<Vote>();

        public virtual ICollection<UserFavoritePhoto> FavoritePhotos { get; set; }
            = new HashSet<UserFavoritePhoto>();
    }
}
