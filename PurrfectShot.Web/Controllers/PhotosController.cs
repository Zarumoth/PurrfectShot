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
            ViewData["Title"] = "Качи нова снимка";
            var model = new PhotoInputModel();

            try
            {
                model.Cats = await catService.GetAllCatsForSelectAsync();

                return View(model);
            }
            catch (Exception)
            {
                TempData["Error"] = "Грешка при зареждане на списъка с котки.";
                return RedirectToAction(nameof(Index), "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Upload(PhotoInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Качи нова снимка";
                model.Cats = await catService.GetAllCatsForSelectAsync();
                return View(model);
            }

            if (!await catService.ExistsByIdAsync(model.CatId))
            {
                ModelState.AddModelError(nameof(model.CatId), "Избраната котка не съществува!");

                ViewData["Title"] = "Качи нова снимка";
                model.Cats = await catService.GetAllCatsForSelectAsync();
                return View(model);
            }

            try
            {
                string wwwrootPath = webHostEnvironment.WebRootPath;
                await photoService.UploadPhotoAsync(model, wwwrootPath);

                TempData["Success"] = "Снимката беше добавена в албума!";
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Грешка при качване на снимката. Опитайте пак.");
                ViewData["Title"] = "Качи нова снимка";
                model.Cats = await catService.GetAllCatsForSelectAsync();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                var photoDetails = await photoService.GetPhotoDetailsAsync(id);

                if (photoDetails == null)
                {
                    TempData["Error"] = "Снимката не беше намерена.";
                    return RedirectToAction(nameof(Index), "Home");
                }

                ViewData["Title"] = $"Снимка на {photoDetails.CatName}";
                return View(photoDetails);
            }
            catch (Exception)
            {
                TempData["Error"] = "Грешка при зареждане на детайлите.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Vote(VoteInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = model.PhotoId });
            }

            try
            {
                await photoService.VoteForPhotoAsync(model);

                TempData["Success"] = "Гласът ти беше приет!";
                return RedirectToAction(nameof(Details), new { id = model.PhotoId });
            }
            catch (Exception)
            {
                TempData["Error"] = "Гласът не беше записан успешно.";
                return RedirectToAction(nameof(Details), new { id = model.PhotoId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetProfilePicture(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                await photoService.SetProfilePicture(id);
                TempData["Success"] = "Профилната снимка е променена!";
            }
            catch (Exception)
            {
                TempData["Error"] = "Грешка при задаване на профилна снимка.";
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                var photoToEdit = await photoService.GetPhotoForEditAsync(id);

                if (photoToEdit == null)
                {
                    TempData["Error"] = "Снимката не съществува.";
                    return RedirectToAction(nameof(Index), "Home");
                }

                ViewData["Title"] = "Редактиране на описанието";
                return View(photoToEdit);
            }
            catch (Exception)
            {
                TempData["Error"] = "Грешка при зареждане на редактора.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PhotoEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Редактиране на описанието";
                return View(model);
            }

            try
            {
                await photoService.UpdatePhotoAsync(model);

                TempData["Success"] = "Описанието беше обновено!";
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (Exception)
            {
                TempData["Error"] = "Грешка при актуализиране на снимката.";
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                var photoToDelete = await photoService.GetPhotoForDeleteAsync(id);

                if (photoToDelete == null)
                {
                    TempData["Error"] = "Снимката вече е била изтрита.";
                    return RedirectToAction(nameof(Index), "Home");
                }

                ViewData["Title"] = "Изтриване на снимка";
                return View(photoToDelete);
            }
            catch (Exception)
            {
                TempData["Error"] = "Грешка при зареждане на страницата за изтриване.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                string webRootPath = webHostEnvironment.WebRootPath;
                int catId = await photoService.DeletePhotoAsync(id, webRootPath);

                if (catId == 0)
                    return RedirectToAction(nameof(Index), "Home");

                TempData["Success"] = "Снимката е изтрита успешно.";
                return RedirectToAction(nameof(Details), "Cats", new { id = catId });
            }
            catch (Exception)
            {
                TempData["Error"] = "Възникна системна грешка при изтриване.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}