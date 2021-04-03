using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET
        [Route("UserSettings/ChangePassword")]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/ChangePassword/Index.cshtml");
        }
    }
}