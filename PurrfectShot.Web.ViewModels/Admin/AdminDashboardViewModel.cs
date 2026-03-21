using PurrfectShot.Web.ViewModels.Cats;
using PurrfectShot.Web.ViewModels.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectShot.Web.ViewModels.Admin
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<CatCardViewModel> Cats { get; set; } = new List<CatCardViewModel>();
        public IEnumerable<UserListViewModel> Users { get; set; } = new List<UserListViewModel>();
        public IEnumerable<PhotoCardViewModel> Photos { get; set; } = new List<PhotoCardViewModel>();
    }
}
