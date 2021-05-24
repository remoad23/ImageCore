using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Services;
using ImageCore.Services.Interfaces;
using ImageCore.ViewModel.UserSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers.UserSettings
{
    public class ChangePhonenumberController : Controller
    {
        
        private UserManager<UserModel> UserManager;
        private ISmsSender SmsAuth;

        public ChangePhonenumberController(UserManager<UserModel> userManager,ISmsSender smsAuth)
        {
            UserManager = userManager;
            SmsAuth = smsAuth;
        }

        
        // GET
        [Authorize]
        [Route("UserSettings/ChangePhonenumber")]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/ChangePhonenumber/Index.cshtml");
        }

        // Patch
        public async Task<IActionResult> Update([FromForm]ChangePhonenumberViewlModel model)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            var token = await UserManager.GenerateChangePhoneNumberTokenAsync(user, model.CurrentPhoneNumber);
            var idk = SmsAuth.SendSmsAsync(user.PhoneNumber, token);
            return View("~/Views/UserSettings/ChangePhonenumber/Update.cshtml");
        }

        public IActionResult Store([FromForm] string token)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync("80daa7e3-48d6-4283-b9c4-cc290fa3e4c0").Result;
            
            if (UserManager.VerifyChangePhoneNumberTokenAsync(user,token,user.PhoneNumber).Result)
            {
                UserManager.ChangePhoneNumberAsync(user,user.PhoneNumber,token);
                return View("~/Views/UserSettings/ChangePhonenumber/Index.cshtml");
            }
            else
            {
                ViewData["error"] = "Invalid Token";
                return View("~/Views/UserSettings/ChangePhonenumber/Update.cshtml");
            }

        }
    }
}