using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;

namespace PurrfectShot.Web.Controllers
{
    public class PhotosController(IPhotoService photoService, ICatService catService, IWebHostEnvironment webHostEnvironment) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            var model = new PhotoInputModel();
            model.Cats = await catService.GetAllCatsForSelectAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PhotoInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Cats = await catService.GetAllCatsForSelectAsync();
                return View(model);
            }

            string wwwrootPath = webHostEnvironment.WebRootPath;
            await photoService.UploadPhotoAsync(model, wwwrootPath);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var photoDetails = await photoService.GetPhotoDetailsAsync(id);

            if (photoDetails == null)
            {
                return NotFound();
            }

            return View(photoDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(VoteInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = model.PhotoId });
            }

            await photoService.VoteForPhotoAsync(model);
            return RedirectToAction("Details", new { id = model.PhotoId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var photoToEdit = await photoService.GetPhotoForEditAsync(id);

            if (photoToEdit == null)
            {
                return NotFound();
            }

            return View(photoToEdit);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PhotoEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await photoService.UpdatePhotoAsync(model);
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var photoToDelete = await photoService.GetPhotoForDeleteAsync(id);

            if (photoToDelete == null)
            {
                return NotFound();
            }

            return View(photoToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = webHostEnvironment.WebRootPath;
            int catId = await photoService.DeletePhotoAsync(id, webRootPath);

            if (catId == 0)
            {
                return NotFound();
            }

            return RedirectToAction("Details", "Cats", new { id = catId });
        }
    }
}