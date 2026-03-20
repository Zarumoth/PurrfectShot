using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PurrfectShot.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        protected string GetUserName()
        {
            return User.Identity?.Name ?? "Котконимен";
        }

        protected bool IsAdmin => User.IsInRole("Administrator");
    }
}
