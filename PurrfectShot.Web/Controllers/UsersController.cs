using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data.Interfaces;
using System.Security.Claims;
using PurrfectShot.Web.ViewModels.Photos;

namespace PurrfectShot.Web.Controllers
{
    [Authorize]
    public class UsersController(IPhotoService photoService, ILogger<UsersController> logger) : BaseController
    {
        public async Task<IActionResult> Favorites()
        {
            ViewData["Title"] = "Моите любими снимки";

            try
            {
                string userId = GetUserId();

                var model = await photoService.GetFavoritePhotosByUserIdAsync(userId);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading favorites for user {UserId}", GetUserId());

                TempData["Error"] = "Възникна грешка при зареждането на вашите любими снимки.";
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> MyPhotos()
        {
            ViewData["Title"] = "Моите снимки";

            try
            {
                string userId = GetUserId();

                var model = await photoService.GetPhotosByUserIdAsync(userId);

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading uploaded photos for user {UserId}", GetUserId());

                TempData["Error"] = "Възникна грешка при зареждането на вашите снимки.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}