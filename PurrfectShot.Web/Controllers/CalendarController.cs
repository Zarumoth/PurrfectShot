using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Common;
using PurrfectShot.Services.Data.Interfaces;
using System.Globalization;

namespace PurrfectShot.Web.Controllers
{
    [AllowAnonymous]
    public class CalendarController(IPhotoService photoService, ILogger<CalendarController> logger) : BaseController
    {

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Архив на календара";

            try
            {
                var model = await photoService.GetCalendarMonthsAsync();
                TempData["Info"] = "Изберете месец, за да видите снимките, качени през този период.";
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading calendar index.");

                TempData["Error"] = "Грешка при зареждане на календара. Моля, опитайте отново.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        public async Task<IActionResult> Month(int year, int month)
        {
            if (year <= 0 || month <= 0)
            {

                return BadRequest();
            }

            try
            {
                var photos = await photoService.GetPhotosByMonthAsync(year, month);
                var monthName = month.ToBulgarianMonthName();

                ViewData["Title"] = $"{monthName} {year}";

                return View(photos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading month gallery for {Month}/{Year}", month, year);

                TempData["Error"] = "Грешка при извличане на снимките за избрания месец.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
