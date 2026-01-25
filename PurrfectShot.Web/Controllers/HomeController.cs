using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels;
using PurrfectShot.Web.ViewModels.Home;
using System.Diagnostics;

namespace PurrfectShot.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatService _catService;

        public HomeController(ICatService catService)
        {
            _catService = catService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeIndexViewModel
            {
                FeaturedCats = await _catService.GetFeaturedCatsAsync()
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
