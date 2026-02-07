using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels;
using PurrfectShot.Web.ViewModels.Home;
using System.Diagnostics;

namespace PurrfectShot.Web.Controllers
{
    public class HomeController (ICatService catService, IPhotoService photoService) : Controller
    {

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "??????";

            try
            {
                var (photos, votes) = await photoService.GetGlobalStatisticsAsync();

                var model = new HomeIndexViewModel
                {
                    FeaturedCats = await catService.GetFeaturedCatsAsync(),
                    TotalPhotos = photos,
                    TotalVotes = votes
                };

                return View(model);
            }
            catch (Exception)
            {
                return View(new HomeIndexViewModel
                {
                    FeaturedCats = await catService.GetFeaturedCatsAsync()
                });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
