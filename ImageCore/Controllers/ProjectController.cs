using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Controllers
{
    public class ProjectController : Controller
    {
        private ContextDb Context;
        private UserManager<UserModel> UserManager;
        
        public ProjectController(ContextDb context,UserManager<UserModel> userManager)
        {
            Context = context;
            UserManager = userManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        
        [Authorize]
        public IActionResult Store(string userId,string name)
        {
            string id = UserManager.GetUserId(User);
            
            ProjectModel project = new ProjectModel
            {
                UserId = id,
                Name = name
            };
            Context.Project.Add(project);
            Context.SaveChanges();
            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        [Authorize]
        public IActionResult Destroy(string projectID)
        {
            ProjectModel project = Context.Project.Find(projectID);
            Context.Project.Remove(project);
            Context.SaveChanges();
            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}
