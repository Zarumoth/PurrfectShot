using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Services.Data;
using PurrfectShot.Services.Data.Interfaces;
using PurrfectShot.Web.ViewModels.Admin;

namespace PurrfectShot.Web.Areas.Admin.Controllers
{
    public class UsersController(IUserService userService, ILogger<UsersController> logger) : AdminBaseController
    {
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Добави нов потребител";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                TempData["Error"] = "Имейлът и паролата са задължителни.";
                return View();
            }

            try
            {
                var result = await userService.CreateUserAsync(email, password);

                if (result.Succeeded)
                {
                    TempData["Success"] = $"Потребителят {email} беше създаден успешно!";
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin", tab = "users" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while creating user {Email}", email);

                TempData["Error"] = "Възникна системна грешка при създаването на потребителя.";
            }

            ViewData["Title"] = "Добави нов потребител";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["Title"] = "Редактирай потребител";

            try
            {
                var model = await userService.GetUserForEditAsync(id);
                if (model == null)
                {
                    TempData["Error"] = "Потребителят не е намерен.";
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin", tab = "users" });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error loading edit view for user {UserId}", id);

                return RedirectToAction("Index", "Dashboard", new { area = "Admin", tab = "users" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Редактирай потребител";
                return View(model);
            }

            try
            {
                var result = await userService.UpdateUserAsync(model);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Потребителят беше обновен успешно!";
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin", tab = "users" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating user {UserId}", model.Id);

                TempData["Error"] = "Грешка при актуализация на данните.";
            }

            ViewData["Title"] = "Редактирай потребител";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var userToDelete = await userService.GetUserForDeleteAsync(id);
            if (userToDelete == null)
                return RedirectToAction("Index", "Dashboard");

            if (userToDelete == null)
            {
                TempData["Error"] = "Потребителят не е намерен.";
                return RedirectToAction("Index", "Dashboard", new { area = "Admin", tab = "users" });
            }

            ViewData["Title"] = "Изтриване на потребител";

            return View(userToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == GetUserId())
            {
                TempData["Error"] = "Не може да изтриеш собствения си акаунт!";
                return RedirectToAction("Index", "Dashboard", new { area = "Admin", tab = "users" });
            }

            try
            {
                bool success = await userService.DeleteUserAsync(id);

                if (success)
                    TempData["Success"] = "Потребителят беше премахнат успешно.";
                else
                    TempData["Error"] = "Потребителят не можеше да бъде изтрит.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Critical error deleting user {UserId}", id);

                TempData["Error"] = "Възникна системна грешка при изтриване.";
            }

            return RedirectToAction("Index", "Dashboard", new { area = "Admin", tab = "users" });
        }
    }
}
