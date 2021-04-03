using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Show()
        {
            return View();
            
        }

        [Route("UserSettings/DeleteAccount")]
        public IActionResult Delete()
        {
            return View("~/Views/UserSettings/DeleteAccount/Index.cshtml");
        }
    }
}
