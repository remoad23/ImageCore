using ImageCore.Models.ViewModel;
using ImageCore.Services;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers
{
    public class ContactAdminController : Controller
    {
        private IMailSend Mail;

        public ContactAdminController(IMailSend mail)
        {
            Mail = mail;
        }
        
        // GET
        [Authorize(Roles="User,Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles="Admin,User")]
        public IActionResult Store([FromForm] ContactAdminViewModel model)
        {
            Mail.SendEmail(model.Message,model.Topic,"imagecore23@gmail.com");
            Mail.SendEmail("Dies ist eine Email Bestätigung,dass deine E-Mail angekommen ist. Wir bitten um Geduld für eine Antwort.",model.Topic,model.Email);
            
            return RedirectToAction("Index",new{emailSent = true});
        }
    }
}