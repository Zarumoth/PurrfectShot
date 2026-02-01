using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;

namespace PurrfectShot.Web.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly ICatService _catService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotosController(
            IPhotoService photoService,
            ICatService catService,
            IWebHostEnvironment webHostEnvironment)
        {
            _photoService = photoService;
            _catService = catService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            var model = new PhotoUploadViewModel();
            model.Cats = await _catService.GetAllCatsForSelectAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PhotoUploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Cats = await _catService.GetAllCatsForSelectAsync();
                return View(model);
            }

            string wwwrootPath = _webHostEnvironment.WebRootPath;
            await _photoService.UploadPhotoAsync(model, wwwrootPath);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var photoDetails = await _photoService.GetPhotoDetailsAsync(id);

            if (photoDetails == null)
            {
                return NotFound();
            }

            return View(photoDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(VoteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = model.PhotoId });
            }

            await _photoService.VoteForPhotoAsync(model);
            return RedirectToAction("Details", new { id = model.PhotoId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var photoToEdit = await _photoService.GetPhotoForEditAsync(id);

            if (photoToEdit == null)
            {
                return NotFound();
            }

            return View(photoToEdit);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PhotoEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _photoService.UpdatePhotoAsync(model);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
    }
}