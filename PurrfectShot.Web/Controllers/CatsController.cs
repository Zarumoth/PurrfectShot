using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Services.Interfaces;
using PurrfectShot.Web.ViewModels.Cats;

namespace PurrfectShot.Web.Controllers
{
    public class CatsController(ICatService catService) : Controller
    {

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Добави ново коте";

            try
            {
                TempData["Info"] = "Попълнете формата, за да регистрирате нов член на бандата.";
                return View(new CatInputModel());
            }
            catch (Exception)
            {
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
                int catId = await catService.AddCatAsync(model);

                TempData["Success"] = $"Котката {model.Name} беше добавена успешно!";
                return RedirectToAction(nameof(Details), new { id = catId });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Неуспешен запис в базата данни. Моля, опитайте отново.");
                ViewData["Title"] = "Добави ново коте";
                return View(model);
            }
        }

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
            catch (Exception)
            {
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
            catch
            {
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
            catch (Exception)
            {
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

                ViewData["Title"] = $"Изтриване на {catToDelete.Name}";
                return View(catToDelete);
            }
            catch
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
                await catService.DeleteCatAsync(id);

                TempData["Success"] = "Котката и всички нейни снимки бяха премахнати успешно.";
                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception)
            {
                TempData["Error"] = "Възникна системна грешка при опит за изтриване.";
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
