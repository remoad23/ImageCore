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
    
    
    public class ChangeEmailController : Controller
    {
        
        private UserManager<UserModel> UserManager;
        private ISmsSender SmsAuth;

        public ChangeEmailController(UserManager<UserModel> userManager,ISmsSender smsAuth)
        {
            UserManager = userManager;
            SmsAuth = smsAuth;
        }
        
        // GET
        [Authorize]
        [Route("UserSettings/ChangeEmail")]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/ChangeEmail/Index.cshtml");
        }
        
        [Authorize]
        public async Task<IActionResult> Update([FromForm]ChangeEmailViewModel model)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            var token = await UserManager.GenerateChangeEmailTokenAsync(user,model.NewEmail);
            var idk = SmsAuth.SendSmsAsync(user.PhoneNumber, token);
            
            return View("~/Views/UserSettings/ChangeEmail/Update.cshtml");
        }
        
        [Authorize]
        public IActionResult Store([FromForm] string token,[FromForm] string email)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync("80daa7e3-48d6-4283-b9c4-cc290fa3e4c0").Result;
            
            // ChangeEmailAsync already validates token
            var result = UserManager.ChangeEmailAsync(user,email,token);
            
            // check if right right token entered
            if (result.Result.Succeeded)
            {
                return View("~/Views/UserSettings/ChangeEmail/Index.cshtml");
            }
            else
            {
                ViewData["error"] = "Invalid Token";
                return View("~/Views/UserSettings/ChangeEmail/Update.cshtml");
            }
            
            return View("~/Views/UserSettings/ChangePhonenumber/Index.cshtml");
        }
    }
}