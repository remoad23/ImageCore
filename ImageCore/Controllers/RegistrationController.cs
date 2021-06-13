using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Controllers
{
    public class RegistrationController : Controller
    {
        private UserManager<UserModel> _userManager;
        private SignInManager<UserModel> _signInManager;
        public RegistrationController(
            UserManager<UserModel> userManager,
            SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Store([FromForm]RegisterViewModel model)
        {
           
            if(ModelState.IsValid)
            {
                UserModel user = new UserModel
                {
                    Email = model.Email,
                    UserName = model.Username
                };
                var createdUser = await _userManager.CreateAsync(user,model.Password);

                // has user been created in successfully?
                if (createdUser.Succeeded)
                {
                    
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index","Home");
                }
            }
            else
            {
                //redirect back if not valid
                ViewData["error"] = "Modelstate invalid";
                return RedirectToAction("Index","Registration");
                
            }
            return RedirectToAction("Index","Registration");
        }
    }
}
