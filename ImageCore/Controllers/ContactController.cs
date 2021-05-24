using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Controllers
{
    public class ContactController : Controller
    {
        private ContextDb Context;
        private UserManager<UserModel> UserManager;
        
        public ContactController(ContextDb context,UserManager<UserModel> userManager)
        {
            Context = context;
            UserManager = userManager;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        

        
        [Authorize]
        [HttpPost]
        [Route("Contact/Store/{id}")]
        public IActionResult Store(string contactId)
        {
            string id = UserManager.GetUserId(User);

            ContactModel contact = new ContactModel
            {
                UserId = id,
                ContactUserId = contactId,
            };
            Context.Contact.Add(contact);
            Context.SaveChanges();
            // return 200 status
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Destroy(string userId)
        {
            ContactModel user = Context.Contact.Find(userId);
            Context.Contact.Remove(user);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
