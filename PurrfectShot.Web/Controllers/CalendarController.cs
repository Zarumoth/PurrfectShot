using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;

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
    }
}
