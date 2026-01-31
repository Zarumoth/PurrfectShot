using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Web.Controllers
{
    public class CatsController : Controller
    {
        private readonly ICatService _catService;

        public CatsController(ICatService catService)
        {
            _catService = catService;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View(new CatFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CatFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _catService.AddCatAsync(model);

            // TODO: Redirect to the cat details page after implementing it
            return RedirectToAction("Index", "Home");
        }

    }
}
