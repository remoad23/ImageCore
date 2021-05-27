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
        private IMailSend EmailSend;

        public ChangeEmailController(UserManager<UserModel> userManager,IMailSend emailSend )
        {
            UserManager = userManager;
            EmailSend = emailSend;
        }
        
        // GET
        [Authorize]
        [Route("UserSettings/ChangeEmail")]
        [ValidateAntiForgeryToken]
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/ChangeEmail/Index.cshtml");
        }
        
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm]ChangeEmailViewModel model)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            
            if (!ModelState.IsValid)
            {
                ViewData["Invalid"] = "Einiger deine Angaben sind nicht korrekt";
                return RedirectToAction("Index");
            }

            var token = await UserManager.GenerateChangeEmailTokenAsync(user,model.NewEmail);
            EmailSend.SendEmail($"Klicke auf dem Bestätigungslin,wenn du deine Email ändern möchtest <br> <a href='{Url.Action("Store","ChangeEmail",new {token = token,email = model.NewEmail},Request.Scheme)}'>E-Mail ändern</a>",
                "Email Verification",
                user.Email);

            ViewData["Confirmation"] = "Es wurde dir eine Bestätigungsemail gesendet.Bitte überprüfe deine Emails.";
            return View("~/Views/UserSettings/ChangeEmail/Index.cshtml");
        }
        
        [Authorize]
        public IActionResult Store([FromQuery] string token,[FromQuery] string email)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            
            // ChangeEmailAsync already validates token
            var result = UserManager.ChangeEmailAsync(user,email,token);

            // check if right right token entered
            if (result.Result.Succeeded)
            {
                return View("~/Views/UserSettings/ChangeEmail/Update.cshtml");
            }
            else
            {
                return View("~/Views/Error/Error.cshtml");
            }
        }
    }
}