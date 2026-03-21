using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectShot.Web.ViewModels.Admin
{
    public class UserListViewModel
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}