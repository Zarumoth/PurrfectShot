using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels;
using PurrfectShot.Web.ViewModels.Home;
using System.Diagnostics;

namespace PurrfectShot.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController (ICatService catService, IPhotoService photoService, ILogger<HomeController> logger) : BaseController
    {

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Начало";

            try
            {
                var (photos, votes) = await photoService.GetGlobalStatisticsAsync();
                var featuredCats = await catService.GetFeaturedCatsAsync();

                var model = new HomeIndexViewModel
                {
                    FeaturedCats = await catService.GetFeaturedCatsAsync(),
                    TotalPhotos = photos,
                    TotalVotes = votes
                };

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Critical error on Home Page Index.");

                return View(new HomeIndexViewModel());
            }
        }
        public IActionResult Privacy()
        {
            ViewData["Title"] = "Политика за поверителност";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
