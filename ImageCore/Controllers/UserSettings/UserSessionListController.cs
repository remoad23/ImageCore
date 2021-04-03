using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers
{
    public class UserSessionListController : Controller
    {
        // GET
        [Route("UserSettings/UserSessionList")]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/UserSessionList/Index.cshtml");
        }
    }
}