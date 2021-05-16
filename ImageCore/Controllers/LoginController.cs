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
        public async Task<IActionResult> login([FromForm]LoginViewModel model)
        {
            Console.WriteLine(model.password);
            Console.WriteLine(model.userName);
            var user = await _signInManager.PasswordSignInAsync(
                model.userName,
                model.password,
                false,
                true
                );

            Console.WriteLine(ModelState.IsValid);
            Console.WriteLine(user.Succeeded);
            if (!ModelState.IsValid || !user.Succeeded) {
                Console.WriteLine("Passt1");
                //redirect back if not valid
                return RedirectToAction("Login","login");
            }
            else if(user.Succeeded){
                Console.WriteLine("Passt2");
                return RedirectToAction("Index","Home");
            }
            else if(user.IsLockedOut) {
                Console.WriteLine("Passt3");
                //redirect back if not valid
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }

            return RedirectToAction("login");
        }
        
        [ValidateAntiForgeryToken]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            RedirectToAction("Index","Login");
        }
    }
}
