using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PurrfectShot.Data;
using PurrfectShot.Data.Models;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Admin;
using PurrfectShot.Web.ViewModels.Cats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectShot.Services.Data
{
    public class UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, ILogger<UserService> logger) : IUserService
    {
        public async Task<IEnumerable<UserListViewModel>> GetAllUsersAsync()
        {
            var users = await userManager.Users.ToListAsync();

            var userViewModels = new List<UserListViewModel>();

            foreach (var user in users)
            {
                var viewModel = mapper.Map<UserListViewModel>(user);

                var roles = await userManager.GetRolesAsync(user);
                viewModel.Role = roles.FirstOrDefault() ?? "Няма роля";

                userViewModels.Add(viewModel);
            }

            return userViewModels;
        }

        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            return await roleManager
                .Roles
                .AsNoTracking()
                .Select(r => r.Name)
                .ToListAsync();
        }

        public async Task<bool> AssignRoleAsync(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null || !await roleManager.RoleExistsAsync(roleName))
                return false;

            var currentRoles = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded;
        }

        public async Task<IdentityResult> CreateUserAsync(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            logger.LogInformation("New user {Email} created by Admin.", email);

            return await userManager.CreateAsync(user, password);
        }

        public async Task<UserEditInputModel?> GetUserForEditAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user == null ? null : mapper.Map<UserEditInputModel>(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserEditInputModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null) return IdentityResult.Failed();

            mapper.Map(model, user);

            user.NormalizedEmail = model.Email.ToUpper();
            user.NormalizedUserName = model.UserName.ToUpper();

            logger.LogInformation("User {UserId} updated.", model.Id);

            return await userManager.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await userManager.DeleteAsync(user);

            logger.LogCritical("User {UserId} was DELETED from the system.", userId);

            return result.Succeeded;
        }
    }
}
