using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data.Interfaces;
using System.Security.Claims;
using PurrfectShot.Web.ViewModels.Photos;

namespace PurrfectShot.Web.Controllers
{
    [Authorize]
    public class UserController(IPhotoService photoService) : Controller
    {
        public async Task<IActionResult> Favorites()
        {
            ViewData["Title"] = "Моите любими снимки";

            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                var model = await photoService.GetFavoritePhotosByUserIdAsync(userId);

                return View(model);
            }
            catch (Exception)
            {
                TempData["Error"] = "Възникна грешка при зареждането на вашите любими снимки.";
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> MyPhotos()
        {
            ViewData["Title"] = "Моите снимки";

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var model = await photoService.GetPhotosByUserIdAsync(userId);

            return View(model);
        }
    }
}