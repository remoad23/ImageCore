using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers
{
    public class AboutUs : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}