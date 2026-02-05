using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;
using System.Globalization;

namespace PurrfectShot.Web.Controllers
{
    public class CalendarController (IPhotoService photoService) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var model = await photoService.GetCalendarMonthsAsync();
            return View(model);
        }

        public async Task<IActionResult> Month(int year, int month)
        {
            var photos = await photoService.GetPhotosByMonthAsync(year, month);

            ViewData["Title"] = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";

            return View(photos);
        }
    }
}
