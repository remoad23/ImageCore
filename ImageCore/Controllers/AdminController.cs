using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel.User;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers
{
    public class AdminController : Controller
    {
        private ContextDb Dbcontext;
        private UserManager<UserModel> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        private IMailSend MailSend;
        
        public AdminController(ContextDb dbContext,
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
        [Authorize]
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
                    UserList = UserManager.GetUsersInRoleAsync("Admin").Result
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
                                Role = "Admin",
                                Image = user.image is not null ? user.image : ""
                            }
                        )
                        .OrderBy(u => u.Username)
                        .Take(10)
                        .ToList();
                }
                else
                {
                    UserList = UserManager.GetUsersInRoleAsync("Admin").Result
                        .Join(
                            Dbcontext.UserRoles,
                            model => model.Id,
                            userRole => userRole.UserId,
                            (user, userRole) => new UserListViewModel
                            {
                                UserId = user.Id,
                                Username = user.UserName,
                                Email = user.Email,
                                Role = "Admin",
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
                    UserList = UserManager.GetUsersInRoleAsync("Admin").Result
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
                                Role = "Admin",
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
                    UserList = UserManager.GetUsersInRoleAsync("Admin").Result
                        .Join(
                            Dbcontext.UserRoles,
                            model => model.Id,
                            userRole => userRole.UserId,
                            (user, userRole) => new UserListViewModel
                            {
                                UserId = user.Id,
                                Username = user.UserName,
                                Email = user.Email,
                                Role = "Admin",
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
    }
}