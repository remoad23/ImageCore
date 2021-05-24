using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Services;
using ImageCore.Services.Interfaces;
using ImageCore.ViewModel.UserSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ImageCore.Controllers.UserSettings
{
    public class AddPhonenumberController : Controller
    {
        private UserManager<UserModel> UserManager;
        private ISmsSender SmsAuth;

        public AddPhonenumberController(UserManager<UserModel> userManager,ISmsSender smsAuth)
        {
            UserManager = userManager;
            SmsAuth = smsAuth;
        }
        
        [Authorize]
        [Route("UserSettings/AddPhonenumber")]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/AddPhonenumber/Index.cshtml");
        }

        [Authorize]
        public async Task<IActionResult> Update([FromForm] string phoneNumber)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            var token = await UserManager.GenerateChangePhoneNumberTokenAsync(user,phoneNumber);
            var idk = SmsAuth.SendSmsAsync(phoneNumber, token);
            
            return View("~/Views/UserSettings/AddPhonenumber/Update.cshtml");
        }

        [Authorize]
        public IActionResult Store([FromForm]AddPhonenumberViewModel model,string token)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync("80daa7e3-48d6-4283-b9c4-cc290fa3e4c0").Result;

            var result = UserManager.VerifyChangePhoneNumberTokenAsync(user, token, user.PhoneNumber);

            // check if right right token entered
            if (result.Result)
            {
                UserManager.ChangePhoneNumberAsync(user,user.PhoneNumber,token);
                return View("~/Views/UserSettings/AddPhonenumber/Index.cshtml");
            }
            else
            {
                ViewData["error"] = "Invalid Token";
                return View("~/Views/UserSettings/AddPhonenumber/Update.cshtml");
            }
        }
    }
}