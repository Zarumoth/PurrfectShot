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

            if (!await catService.ExistsByIdAsync(model.CatId))
            {
                ModelState.AddModelError(nameof(model.CatId), "Избраната котка не съществува в системата!");

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
            if (id <= 0)
            {
                return BadRequest();
            }

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

        [HttpPost]
        public async Task<IActionResult> SetProfilePicture(int id)
        {
            if (id <= 0)
                return BadRequest();

            await photoService.SetProfilePicture(id);

            //TODO: Imlement success messages in the other controllers and views as well
            //TODO: Try-Catch galore, implement it in all methods that interact with the database and file system, to prevent crashes and show user-friendly error messages
            try
            {
                await photoService.SetProfilePicture(id);
                TempData["Success"] = "Профилната снимка беше зададена!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Не успяхме да зададем профилна снимка.";
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

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
            if (id <= 0)
            {
                return BadRequest();
            }

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
            if (id <= 0)
            {
                return BadRequest();
            }

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