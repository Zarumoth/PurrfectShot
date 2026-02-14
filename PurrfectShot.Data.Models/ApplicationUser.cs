using Microsoft.AspNetCore.Identity;

namespace PurrfectShot.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Cat> OwnedCats { get; set; } 
            = new HashSet<Cat>();

        public virtual ICollection<Vote> Votes { get; set; }
            = new HashSet<Vote>();

    }
}
