using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Admin;
using System.Security.Claims;

namespace PurrfectShot.Web.Areas.Admin.Controllers
{
    public class DashboardController(ICatService catService, IUserService userService, IPhotoService photoService, ILogger<DashboardController> logger) : AdminBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index(string tab = "cats")
        {
            ViewBag.ActiveTab = tab;

            try
            {
                var model = new AdminDashboardViewModel
                {
                    Cats = await catService.GetAllCatsForAdminAsync(),
                    Users = await userService.GetAllUsersAsync(),
                    Photos = await photoService.GetAllPhotosForAdminAsync()
                };

                ViewBag.AllRoles = await userService.GetAllRolesAsync();

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading Admin Hub.");

                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                await catService.RestoreArchivedCatAsync(id);

                TempData["Success"] = "Профилът на котката беше възстановен успешно!";
            }
            catch (Exception)
            {
                TempData["Error"] = "Възникна грешка при възстановяването на профила.";
            }

            return RedirectToAction(nameof(Index), new { tab = "cats" });
        }

        //[HttpPost]
        //public async Task<IActionResult> DeletePermanently(int id)
        //{
        //    if (id <= 0)
        //        return BadRequest();

        //    await catService.DeleteCatPermanentlyAsync(id);
        //    TempData["Success"] = "Котката и всички нейни данни бяха изтрити ЗАВИНАГИ.";

        //    return RedirectToAction(nameof(Index), new { tab = "cats" });
        //}

        [HttpPost]
        public async Task<IActionResult> Archive(int id)
        {
            if (id <= 0) return BadRequest();

            try
            {
                await catService.ArchiveCatAsync(id);
                TempData["Success"] = "Профилът на котката беше архивиран успешно!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Грешка при архивиране на котка с ID: {Id}", id);

                TempData["Error"] = "Възникна грешка при архивирането.";
            }

            return RedirectToAction(nameof(Index), new { tab = "cats" });
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

            return RedirectToAction(nameof(Index), new { tab = "users" });
        }
    }
}