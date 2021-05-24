using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Services;
using ImageCore.Services.Interfaces;
using ImageCore.ViewModel.UserSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers
{
    public class ChangePasswordController : Controller
    {
        
        private UserManager<UserModel> UserManager;
        private ISmsSender SmsAuth;

        public ChangePasswordController(UserManager<UserModel> userManager,ISmsSender smsAuth)
        {
            UserManager = userManager;
            SmsAuth = smsAuth;
        }

        
        [Authorize]
        [Route("UserSettings/ChangePassword")]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/ChangePassword/Index.cshtml");
        }

        [Authorize]
        public async Task<IActionResult> Update([FromForm]ChangePasswordViewModel model)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            var token = await UserManager.GeneratePasswordResetTokenAsync(user);
            var idk = SmsAuth.SendSmsAsync(user.PhoneNumber, token);
            
            return View("~/Views/UserSettings/ChangePassword/Update.cshtml");
        }
        
        [Authorize]
        public IActionResult Store([FromForm] string token,[FromForm] string password)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync("80daa7e3-48d6-4283-b9c4-cc290fa3e4c0").Result;
            
            // ResetPasswordAsync already validates token
            var result = UserManager.ResetPasswordAsync(user,token,password);

            // check if right right token entered
            if (result.Result.Succeeded)
            {
               return View("~/Views/UserSettings/ChangePassword/Index.cshtml"); 
            }
            else
            {
                ViewData["error"] = "Invalid Token";
                return View("~/Views/UserSettings/ChangePassword/Update.cshtml");
            }
            
        }
        
    }
}