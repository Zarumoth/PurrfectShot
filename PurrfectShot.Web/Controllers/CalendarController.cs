using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;
using System.Globalization;

namespace PurrfectShot.Web.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IPhotoService _photoService;

        public CalendarController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _photoService.GetCalendarMonthsAsync();
            return View(model);
        }

        public async Task<IActionResult> Month(int year, int month)
        {
            var photos = await _photoService.GetPhotosByMonthAsync(year, month);

            ViewData["Title"] = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}";

            return View(photos);
        }
    }
}
