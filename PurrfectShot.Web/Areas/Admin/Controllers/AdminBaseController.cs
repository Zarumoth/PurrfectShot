using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurrfectShot.Web.Controllers;

namespace PurrfectShot.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public abstract class AdminBaseController : BaseController
    {
    }
}