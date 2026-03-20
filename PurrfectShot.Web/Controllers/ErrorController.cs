using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PurrfectShot.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : BaseController
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpCodeHandler(int statusCode)
        {
            if (statusCode == 404)
                return View("NotFound");

            if (statusCode == 400)
                return View("BadRequest");

            if (statusCode == 403)
                return View("AccessDenied");

            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ExceptionHandler()
        {
            return View("Error");
        }
    }
}
