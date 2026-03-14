using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data.Interfaces;

namespace PurrfectShot.Web.Areas.Admin.Controllers
{
    public class DashboardController(ICatService catService) : AdminBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Контролен Панел";
            var model = await catService.GetAllCatsForAdminAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            if (id <= 0) return BadRequest();

            try
            {
                await catService.RestoreArchivedCatAsync(id);

                TempData["Success"] = "Профилът на котката беше възстановен успешно!";
            }
            catch (Exception)
            {
                TempData["Error"] = "Възникна грешка при възстановяването на профила.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePermanently(int id)
        {
            if (id <= 0) return BadRequest();

            await catService.DeleteCatPermanentlyAsync(id);
            TempData["Success"] = "Котката и всички нейни данни бяха изтрити ЗАВИНАГИ.";

            return RedirectToAction(nameof(Index));
        }
    }
}