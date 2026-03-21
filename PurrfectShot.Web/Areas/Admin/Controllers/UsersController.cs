using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.Controllers;
using System.Security.Claims;

namespace PurrfectShot.Web.Areas.Admin.Controllers
{
    public class UsersController (IUserService userService, ILogger<UsersController> logger) : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await userService.GetAllUsersAsync();

                ViewBag.AllRoles = await userService.GetAllRolesAsync();

                return View(users);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading user management dashboard.");

                return RedirectToAction("Index", "Dashboard");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            try
            {
                bool success = await userService.AssignRoleAsync(userId, roleName);

                if (success)
                {
                    logger.LogInformation("Admin {AdminId} changed role of user {UserId} to {Role}",
                        User.FindFirstValue(ClaimTypes.NameIdentifier), userId, roleName);

                    TempData["Success"] = "Ролята беше актуализирана!";
                }
                else
                {
                    TempData["Error"] = "Неуспешна промяна на роля.";
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error assigning role {Role} to user {UserId}", roleName, userId);

                TempData["Error"] = "Възникна системна грешка.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
