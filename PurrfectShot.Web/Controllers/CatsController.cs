using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Web.Controllers
{
    [Authorize]
    public class CatsController(ICatService catService, IPhotoService photoService, ILogger<CatsController> logger) : BaseController
    {
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var featuredCats = await catService.GetFeaturedCatsAsync();
                ViewData["Title"] = "Нашите котки";
                return View(featuredCats);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while loading featured cats list.");

                TempData["Error"] = "Възникна грешка при зареждането на котките. Моля, опитайте отново по-късно.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Добави ново коте";

            try
            {
                TempData["Info"] = "Попълнете формата, за да регистрирате нов член на бандата.";
                return View(new CatInputModel());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading the 'Add Cat' form.");

                TempData["Error"] = "Възникна грешка при зареждането на формата.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(CatInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Добави ново коте";
                return View(model);
            }

            try
            {
                var userId = GetUserId();
                int catId = await catService.AddCatAsync(model, userId);

                TempData["Success"] = $"Котката {model.Name} беше добавена успешно!";
                return RedirectToAction(nameof(Details), new { id = catId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Database error while adding new cat: {CatName}", model.Name);

                ModelState.AddModelError(string.Empty, "Неуспешен запис в базата данни. Моля, опитайте отново.");
                ViewData["Title"] = "Добави ново коте";
                return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                var catDetails = await catService.GetCatDetailsAsync(id);

                if (catDetails == null)
                {
                    TempData["Error"] = "Котката не беше намерена.";
                    return RedirectToAction(nameof(Index), "Home");
                }

                ViewData["Title"] = $"Профил на {catDetails.Name}";
                return View(catDetails);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Critical error loading details for CatId: {CatId}", id);

                TempData["Error"] = "Възникна системна грешка при зареждане на профила.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                var catToEdit = await catService.GetCatForEditAsync(id);

                if (catToEdit == null)
                {
                    TempData["Error"] = "Котката, която искате да редактирате, не съществува.";
                    return RedirectToAction(nameof(Index), "Home");
                }

                ViewData["Title"] = $"Редактиране на {catToEdit.Name}";
                return View(catToEdit);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading edit form for CatId: {CatId}", id);

                TempData["Error"] = "Грешка при извличане на данните за редакция.";
                return RedirectToAction(nameof(Index), "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(CatEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = $"Редактиране на {model.Name}";
                return View(model);
            }

            try
            {
                await catService.UpdateCatAsync(model);

                TempData["Success"] = $"Профилът на {model.Name} беше обновен успешно!";
                return RedirectToAction(nameof(Details), new { id = model.Id });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating CatId: {CatId}", model.Id);

                ModelState.AddModelError(string.Empty, "Грешка при актуализиране на данните в базата.");
                ViewData["Title"] = $"Редактиране на {model.Name}";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            try
            {
                var catToDelete = await catService.GetCatForDeleteAsync(id);

                if (catToDelete == null)
                {
                    TempData["Error"] = "Котката вече е била изтрита или не съществува.";
                    return RedirectToAction(nameof(Index), "Home");
                }

                ViewData["PhotoCount"] = await photoService.GetPhotoCountByCatIdAsync(id);

                ViewData["Title"] = $"Изтриване на {catToDelete.Name}";
                return View(catToDelete);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading delete confirmation for CatId: {CatId}", id);

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
                var hasPhotos = await photoService.GetPhotoCountByCatIdAsync(id) > 0;

                if (hasPhotos)
                {
                    await catService.ArchiveCatAsync(id);
                    TempData["Success"] = "Профилът е архивиран. Снимките остават в календара!";
                }
                else
                {
                    await catService.DeleteCatPermanentlyAsync(id);
                    TempData["Success"] = "Записът беше изтрит окончателно.";
                }

                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error finalizing delete/archive for CatId: {CatId}", id);

                TempData["Error"] = "Възникна грешка при операцията.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
