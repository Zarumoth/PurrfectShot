using Microsoft.AspNetCore.Identity;
using PurrfectShot.Web.ViewModels.Admin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectShot.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetAllUsersAsync();

        Task<bool> AssignRoleAsync(string userId, string roleName);

        Task<IEnumerable<string>> GetAllRolesAsync();

        Task<IdentityResult> CreateUserAsync(string email, string password);

        Task<UserEditInputModel?> GetUserForEditAsync(string userId);

        Task<IdentityResult> UpdateUserAsync(UserEditInputModel model);

        Task<bool> DeleteUserAsync(string userId);

    }
}
