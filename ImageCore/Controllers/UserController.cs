using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel.User;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Controllers
{
    public class UserController : Controller
    {
        private ContextDb Dbcontext;
        private UserManager<UserModel> UserManager;
        private RoleManager<UserModel> RoleManager;
        private IMailSend MailSend;
        
        public UserController(ContextDb dbContext,
            UserManager<UserModel> userManager,
            IMailSend mailSend,
            RoleManager<UserModel> roleManager)
        {
            Dbcontext = dbContext;
            UserManager = userManager;
            MailSend = mailSend;
            RoleManager = roleManager;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Route("User/Edit/{id}")]
        [Authorize]
        public IActionResult Update(string id)
        {
            var user = Dbcontext.Users.Find(id);

            var userEditViewModel = new UserEditViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                File = null,
                Password = user.PasswordHash,
                Role = "Admin"
            };
            
            ViewData["id"] = id;
            return View(userEditViewModel);
        }

        [Route("User/Edit/Store/{id}")]
        [Authorize]
        public IActionResult Put(string id,UserEditViewModel model)
        {
            var user = Dbcontext.Users.Find(id);

            user.Email = model.Email;
            user.UserName = model.Username;
            UserManager.ChangePasswordAsync(user, user.PasswordHash, model.Password);
            if (model.Role.Equals("User"))
            {
                UserManager.AddToRoleAsync(user, "User");
            }
            else if(model.Role.Equals("Admin"))
            {
                UserManager.AddToRoleAsync(user, "Admin");
            }

            Dbcontext.SaveChanges();
            
            ViewData["UserSaved"] = "Der Benutzer wurde erfolgreich gespeichert";
            return RedirectToAction("Update");
        }
        
        [Authorize]
        [Route("User/{id}")]
        public async Task<IActionResult> Show(string id)
        {
            Console.WriteLine(id);
            // the user that is queried
            var user = await UserManager.FindByIdAsync(id);

            Console.WriteLine(user);
            // the current User
            string authUserId = UserManager.GetUserId(User);
            string authUserName = UserManager.GetUserName(User);

            // are they contacts of each other?
            var contact = Dbcontext.Contact.
                Where(u => u.UserId.Equals(id) && u.ContactUserId.Equals(authUserId) 
                           || u.UserId.Equals(authUserId) && u.ContactUserId.Equals(id)).SingleOrDefault();

            UserViewModel uservm = new UserViewModel
            {
                UserId = user.Id,
                Username = id == authUserId ? authUserName : user.UserName,
                isUser = id == authUserId ? true : false,
                isContact = contact is not null
            };

            if (uservm.isContact)
            {
                ViewData["contactAddUrl"] =
                    Url.Action("Destroy", "Contact", new {contactId = contact.ContactId}, Request.Scheme);
            }
            else
            {
                ViewData["contactAddUrl"] =
                    Url.Action("Store", "Contact", new {contactId = uservm.UserId}, Request.Scheme);
            }
            
            ViewData["RequestScheme"] = Request.Scheme;
            
            return View("~/Views/User/Show.cshtml",uservm);
            
        }

        [Authorize]
        [Route("DeleteAccount")] // tk = token
        public IActionResult DeleteOwnAccount([FromQuery]string token)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;
            

            if (UserManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider,"Deletion",token).Result)
            {
                Console.WriteLine("test");
                Dbcontext.Users.Remove(user);
                Dbcontext.SaveChanges();
            }
            return RedirectToAction("Logout", "Login");
        }
        
        [Authorize]
        [Route("DeleteAnotherAccount")]
        public IActionResult DeleteAnotherAccount(string token)
        {
            string id = UserManager.GetUserId(User);
            UserModel user = UserManager.FindByIdAsync(id).Result;

            if (UserManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider,"Deletion",token).Result)
            {
                Dbcontext.Users.Remove(user);
                Dbcontext.SaveChanges();
            }
            return RedirectToAction("Logout", "Login");
        }
    }
}
