using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;
using System.Globalization;

namespace PurrfectShot.Web.Controllers
{
    public class CalendarController(IPhotoService photoService) : Controller
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
            catch (Exception)
            {
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

                ViewData["Title"] = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";

                return View(photos);
            }
            catch (Exception)
            {
                TempData["Error"] = "Грешка при извличане на снимките за избрания месец.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
