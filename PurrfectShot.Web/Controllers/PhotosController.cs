using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Photos;
using PurrfectShot.Web.ViewModels.Votes;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace PurrfectShot.Web.Controllers
{
    [Authorize]
    public class PhotosController(IPhotoService photoService, ICatService catService, IWebHostEnvironment webHostEnvironment, ILogger<PhotosController> logger) : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Upload(int? catId)
        {
            ViewData["Title"] = "Качи нова снимка";
            var model = new PhotoInputModel();

            try
            {
                if (catId.HasValue && catId > 0)
                {
                    model.CatId = catId.Value;
                }

                model.Cats = await catService.GetAllCatsForSelectAsync();

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading cat list for photo upload.");
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

            string userId = GetUserId();

            try
            {
                string wwwrootPath = webHostEnvironment.WebRootPath;
                await photoService.UploadPhotoAsync(model, userId, wwwrootPath);

                TempData["Success"] = "Снимката беше добавена в албума!";
                return RedirectToAction(nameof(Details), "Cats", new { id = model.CatId });
                //return RedirectToAction(nameof(Index), "Home");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(model.ImageFile), ex.Message);

                model.Cats = await catService.GetAllCatsForSelectAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error occurred during photo upload for CatId: {CatId}", model.CatId);

                ModelState.AddModelError(string.Empty, "Грешка при качване на снимката. Опитайте пак.");
                ViewData["Title"] = "Качи нова снимка";
                model.Cats = await catService.GetAllCatsForSelectAsync();
                return View(model);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            string userId = GetUserId();

            try
            {
                var photoDetails = await photoService.GetPhotoDetailsAsync(id, userId);

                if (photoDetails == null)
                {
                    TempData["Error"] = "Снимката не беше намерена.";
                    return RedirectToAction(nameof(Index), "Home");
                }

                ViewData["Title"] = $"Снимка на {photoDetails.CatName}";
                return View(photoDetails);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving details for photo {PhotoId}", id);
                TempData["Error"] = "Грешка при зареждане на детайлите.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Vote(VoteInputModel model)
        {

            string userId = GetUserId();
            string userName = GetUserName();

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Невалидни данни при гласуване.";
                return RedirectToAction(nameof(Details), new { id = model.PhotoId });
            }

            try
            {
                await photoService.VoteForPhotoAsync(model, userId, userName);

                TempData["Success"] = "Гласът ти беше записан!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error casting vote for photo {PhotoId} by user {UserId}", model.PhotoId, userId);

                TempData["Error"] = "Възникна грешка при записа.";
            }

            return RedirectToAction(nameof(Details), new { id = model.PhotoId });
        }

        [HttpPost]
        public async Task<IActionResult> Unvote(Guid photoId)
        {

            string userName = GetUserName();

            try
            {
                bool isRemoved = await photoService.RemovePhotoVoteAsync(photoId, userName);

                if (isRemoved) TempData["Success"] = "Гласът ти беше премахнат!";
                else TempData["Error"] = "Не открихме твой глас за тази снимка.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error removing vote for photo {PhotoId} by user {UserName}", photoId, userName);

                TempData["Error"] = "Възникна грешка при премахването на гласа.";
            }

            return RedirectToAction(nameof(Details), new { id = photoId });
        }

        [HttpPost]
        public async Task<IActionResult> SetProfilePicture(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            try
            {
                await photoService.SetProfilePicture(id);
                TempData["Success"] = "Профилната снимка е променена!";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error setting profile picture for photo {PhotoId}", id);
                TempData["Error"] = "Грешка при задаване на профилна снимка.";
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading photo edit view for {PhotoId}", id);

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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating photo {PhotoId}", model.Id);

                TempData["Error"] = "Грешка при актуализиране на снимката.";
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            try
            {
                string webRootPath = webHostEnvironment.WebRootPath;
                int catId = await photoService.DeletePhotoAsync(id, webRootPath);

                if (catId == 0)
                    return RedirectToAction(nameof(Index), "Home");

                TempData["Success"] = "Снимката е изтрита успешно.";

                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin", tab = "photos" });
                }

                return RedirectToAction(nameof(Details), "Cats", new { id = catId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting photo {PhotoId}", id);

                TempData["Error"] = "Възникна системна грешка при изтриване.";
                return RedirectToAction(nameof(Index), "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Favorite(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            string userId = GetUserId();

            try
            {
                bool isNowFavorite = await photoService.ToggleFavoriteAsync(id, userId);

                if (isNowFavorite)
                    TempData["Success"] = "Добавено в Любими! ❤️";
                else
                    TempData["Info"] = "Премахнато от Любими.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error toggling favorite for photo {PhotoId} and user {UserId}", id, userId);

                TempData["Error"] = "Възникна грешка.";
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}