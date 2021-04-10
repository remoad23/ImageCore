using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Controllers
{
    public class LoginController : Controller
    {
        private SignInManager<UserModel> _signInManager;

        public LoginController(SignInManager<UserModel> signInManager)
        {
            _signInManager = signInManager;
        }
        
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task login([FromForm]LoginViewModel model)
        {
            var user = await _signInManager
                .PasswordSignInAsync(model.email, model.password,false,true);
            
            if (!ModelState.IsValid || !user.Succeeded) {
                //redirect back if not valid
                Redirect(HttpContext.Request.Headers["Referer"]);
            }
            else if(user.Succeeded){
                RedirectToAction("Index","Home");
            }
            else if(user.IsLockedOut) {
                //redirect back if not valid
                Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }

        public async Task logout()
        {
            await _signInManager.SignOutAsync();
            RedirectToAction("Index","Login");
        }
    }
}
