using AutoMapper;
using PurrfectShot.Data.Models;
using PurrfectShot.Web.ViewModels.Admin;

namespace PurrfectShot.Web.Infrastructure.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<ApplicationUser, UserListViewModel>()
                .ForMember(d => d.Role, opt => opt.Ignore());
        }
    }
}
