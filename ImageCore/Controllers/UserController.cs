using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Controllers
{
    public class UserController : Controller
    {
        private ContextDb Dbcontext;
        private UserManager<UserModel> UserManager;
        
        public UserController(ContextDb dbContext,UserManager<UserModel> userManager)
        {
            Dbcontext = dbContext;
            UserManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
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
                           || u.UserId.Equals(authUserId) && u.ContactUserId.Equals(id));

            UserViewModel uservm = new UserViewModel
            {
                UserId = user.Id,
                Username = id == authUserId ? authUserName : user.UserName,
                isUser = id == authUserId ? true : false,
                isContact = contact.Any()
            };
            
            return View("~/Views/User/Show.cshtml",uservm);
            
        }

        [Authorize]
        [Route("UserSettings/DeleteAccount")]
        public IActionResult Delete()
        {
            return View("~/Views/UserSettings/DeleteAccount/Index.cshtml");
        }
    }
}
