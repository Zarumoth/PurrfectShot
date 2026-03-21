using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectShot.Web.ViewModels.Admin
{
    public class UserEditInputModel
    {
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Потребителското име е задължително.")]
        public string UserName { get; set; } = null!;
    }
}
