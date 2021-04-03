using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers
{
    public class ChangeEmailController : Controller
    {
        // GET
        [Route("UserSettings/ChangeEmail")]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/ChangeEmail/Index.cshtml");
        }
    }
}