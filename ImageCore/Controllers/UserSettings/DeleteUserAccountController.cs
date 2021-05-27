using System;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Services;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers
{
    public class DeleteUserAccountController : Controller
    {
        private UserManager<UserModel> UserManager;
        private IMailSend MailSend;
        
        public DeleteUserAccountController(UserManager<UserModel> userManager,IMailSend mailSend)
        {
            UserManager = userManager;
            MailSend = mailSend;
        }
        
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {
            return View("~/Views/UserSettings/DeleteAccount/Index.cshtml");
        }

        [Authorize]
        [Route("Usersettings/DeleteAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Store(string password)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;

            if (!UserManager.CheckPasswordAsync(user, password).Result)
            {
                ViewData["Notification"] = "Das Passwort stimmt nicht überein";
                return RedirectToAction("Index");
            }

            var token = await UserManager.GenerateUserTokenAsync(user,TokenOptions.DefaultProvider,"Deletion");
            
            MailSend.SendEmail(
                $"Klicke auf dem folgenden Link,wenn du dein Account löschen möchtest: \n  <a href='{Url.Action("DeleteOwnAccount","User",new {token = token},Request.Scheme)}'>Account löschen</a>", 
                "Account löschen",
                user.Email);

            ViewData["Notification"] = "Wir haben dir eine Email gesendet.Bitte bestätige die Email,um deinen Account zu löschen.";
            return RedirectToAction("Index");
        }
    }
}