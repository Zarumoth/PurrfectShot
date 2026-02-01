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

            int catId = await _catService.AddCatAsync(model);
            return RedirectToAction(nameof(Details), new { id = catId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var catDetails = await _catService.GetCatDetailsAsync(id);
            if (catDetails == null)
            {
                return NotFound();
            }

            return View(catDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var catToEdit = await _catService.GetCatForEditAsync(id);

            if (catToEdit == null)
            {
                return NotFound();
            }

            return View(catToEdit);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(CatEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _catService.UpdateCatAsync(model);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
    }
}
