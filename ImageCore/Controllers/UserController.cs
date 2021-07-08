using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private RoleManager<IdentityRole> RoleManager;
        private IMailSend MailSend;
        
        public UserController(ContextDb dbContext,
            UserManager<UserModel> userManager,
            IMailSend mailSend,
            RoleManager<IdentityRole> roleManager)
        {
            Dbcontext = dbContext;
            UserManager = userManager;
            MailSend = mailSend;
            RoleManager = roleManager;
        }
        
        #nullable enable
        [Authorize(Roles="Admin")]
        public IActionResult Index([FromQuery]string? query,[FromQuery]int? pagination,[FromQuery] string descend)
        {
            var UserList = (dynamic)null;
            ViewData["RequestScheme"] = Request.Scheme;
            int pag = (Dbcontext.Users.Count() / 10);
            ViewData["paginationMax"] = (pagination + 5) > pag ? pag : pagination + 5;
            ViewData["paginationMin"] = (pagination - 5) < 0 ? 0 : pagination - 5;
            if (pagination is null)
            {
                if (query is not null)
                {
                    UserList = UserManager.GetUsersInRoleAsync("User").Result
                        .Where(u => u.UserName.Contains(query) || u.UserName.ToLower().Contains(query))
                        .Join(
                            Dbcontext.UserRoles,
                            model => model.Id,
                            userRole => userRole.UserId,
                            (user, userRole) => new UserListViewModel
                            {
                                UserId = user.Id,
                                Username = user.UserName,
                                Email = user.Email,
                                Role = "User",
                                Image = user.image is not null ? user.image : ""
                            }
                        )
                        .OrderBy(u => u.Username)
                        .Take(10)
                        .ToList();
                }
                else
                {
                    UserList = UserManager.GetUsersInRoleAsync("User").Result
                        .Join(
                            Dbcontext.UserRoles,
                            model => model.Id,
                            userRole => userRole.UserId,
                            (user, userRole) => new UserListViewModel
                            {
                                UserId = user.Id,
                                Username = user.UserName,
                                Email = user.Email,
                                Role = "User",
                                Image = user.image is not null ? user.image : ""
                            }
                        )
                        .OrderBy(u => u.Username)
                        .Take(10)
                        .ToList();
                }
               
            }
            else
            {
                if (query is not null)
                {
                    UserList = UserManager.GetUsersInRoleAsync("User").Result
                        .Where(u => u.UserName.Contains(query) || u.UserName.ToLower().Contains(query))
                        .Join(
                            Dbcontext.UserRoles,
                            model => model.Id,
                            userRole => userRole.UserId,
                            (user, userRole) => new UserListViewModel
                            {
                                UserId = user.Id,
                                Username = user.UserName,
                                Email = user.Email,
                                Role = "User",
                                Image = user.image is not null ? user.image : ""
                            }
                        )
                        .OrderBy(u => u.Username)
                        .Skip(10 * (int) pagination)
                        .Take(10)
                        .ToList();
                    Console.WriteLine(query);
                }
                else
                {
                    UserList = UserManager.GetUsersInRoleAsync("User").Result
                        .Join(
                            Dbcontext.UserRoles,
                            model => model.Id,
                            userRole => userRole.UserId,
                            (user, userRole) => new UserListViewModel
                            {
                                UserId = user.Id,
                                Username = user.UserName,
                                Email = user.Email,
                                Role = "User",
                                Image = user.image is not null ? user.image : ""
                            }
                        )
                        .OrderBy(u => u.Username)
                        .Skip(10 * (int) pagination)
                        .Take(10)
                        .ToList();
                }
            }

            var list = new List<UserListViewModel>();
            if (descend is not null)
            {
                if (descend.Equals("true")) list = ((List<UserListViewModel>) UserList).OrderByDescending(u => u.Username).ToList();
                return View(list);
            }
            else
            {
                return View(UserList);
            }
            
        }
        #nullable disable

        [Route("User/Edit/{id}")]
        [Authorize]
        public IActionResult Update(string id)
        {
            ViewData["RequestScheme"] = Request.Scheme;
            var user = Dbcontext.Users.Find(id);

            var role = UserManager.GetRolesAsync(user).Result;

            var userEditViewModel = new UserEditViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                File = null,
                Password = user.PasswordHash,
                Role = role.Contains("Admin") ? "Admin" : "User",
                UserImage = user.image,
            };
            
            ViewData["id"] = id;
            return View(userEditViewModel);
        }

        [Route("User/Edit/Store/{id}")]        
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Put(string id,[FromForm]UserEditViewModel model)
        {
            var user = Dbcontext.Users.Find(id);

            if(model.Email is not null)  
                user.Email = model.Email;
            
            if(model.Username is not null)   
                user.UserName = model.Username;
            
            if(model.Password is not null) 
                await UserManager.ChangePasswordAsync(user, user.PasswordHash, model.Password);
            
            Console.WriteLine("Role:   "+model.Role);

            if (model.Role.Equals("User"))
            {
                 await UserManager.RemoveFromRoleAsync(user, "Admin");
                 await UserManager.AddToRoleAsync(user, "User");
            }
            else if (model.Role.Equals("Admin"))
            {
                await UserManager.RemoveFromRoleAsync(user, "User");
                await UserManager.AddToRoleAsync(user, "Admin");
            }
            
            

            Dbcontext.SaveChanges();
            
            ViewData["UserSaved"] = "Der Benutzer wurde erfolgreich gespeichert";
            return RedirectToAction("Update",new{id=id});
        }
        
        
        [Authorize(Roles="User,Admin")]
        [Route("User/{id}")]
        public async Task<IActionResult> Show(string id)
        {
            // the user that is queried
            var user = await UserManager.FindByIdAsync(id);

            // the current User
            string authUserId = UserManager.GetUserId(User);
            string authUserName = UserManager.GetUserName(User);

            // are they contacts of each other?
            var contact = Dbcontext.Contact.
                Where(u => u.UserId.Equals(id) && u.ContactUserId.Equals(authUserId) 
                           || u.UserId.Equals(authUserId) && u.ContactUserId.Equals(id)).SingleOrDefault();

            var projects = Dbcontext.Project
                .Where(p => p.UserId.Equals(id))
                .Select(p => p.Name)
                .ToList();

            
            UserViewModel uservm = new UserViewModel
            {
                UserId = user.Id,
                Username = id == authUserId ? authUserName : user.UserName,
                isUser = id == authUserId ? true : false,
                isContact = contact is not null,
                ProjectNames = projects,
                Image = user.image,
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
        
        [Authorize(Roles="Admin")]
        [Route("DeleteAnotherAccount/{id}")]
        public IActionResult DeleteAnotherAccount(string id)
        {
            var user = Dbcontext.Users.Find(id);
            Dbcontext.Users.Remove(user);
            Dbcontext.SaveChanges();
            TempData["interactionstatus"] = "User wurde erfolgreich gelöscht";
            return RedirectToAction("Index",new{pagination = 0});
        }
    }
}
