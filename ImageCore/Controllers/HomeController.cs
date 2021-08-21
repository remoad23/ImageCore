using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ImageCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
