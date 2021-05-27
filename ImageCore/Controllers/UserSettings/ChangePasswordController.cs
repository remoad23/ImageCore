using System;
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
        private IMailSend EmailSend;

        public ChangePasswordController(UserManager<UserModel> userManager,IMailSend emailSend)
        {
            UserManager = userManager;
            EmailSend = emailSend;
        }

        
        [Authorize]
        [Route("UserSettings/ChangePassword")]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/ChangePassword/Index.cshtml");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]ChangePasswordViewModel model)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            
            if (!UserManager.CheckPasswordAsync(user, model.CurrentPassword).Result)
            {
                ViewData["Notification"] = "Das Passwort stimmt nicht überein";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                ViewData["Invalid"] = "Einiger deine Angaben sind nicht korrekt";
                return RedirectToAction("Index");
            }
            
            Console.WriteLine(ViewData.Keys);
            
            var token = await UserManager.GeneratePasswordResetTokenAsync(user);
            EmailSend.SendEmail($"Klicke auf die Bestätigungsemail, wenn du dein Passwort änder möchtest: <br>  <a href='{Url.Action("Store","ChangePassword",new {token = token,password = model.NewPassword},Request.Scheme)}'>Passwort ändern</a>", 
                "Passwort zurücksetzen",
                user.Email);
            
            ViewData["Confirmation"] = "Es wurde dir eine Bestätigungsemail gesendet.Bitte überprüfe deine Emails.";
            return View("~/Views/UserSettings/ChangePassword/Index.cshtml");
        }
        
        [Authorize]
        public IActionResult Store([FromQuery] string token,[FromQuery] string password)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            
            // ResetPasswordAsync already validates token
            var result = UserManager.ResetPasswordAsync(user,token,password);

            // check if right right token entered
            if (result.Result.Succeeded)
            {
                return View("~/Views/UserSettings/ChangePassword/Update.cshtml"); 
            }
            else
            {
                return View("~/Views/Error/Error.cshtml");
            }
        }
        
    }
}