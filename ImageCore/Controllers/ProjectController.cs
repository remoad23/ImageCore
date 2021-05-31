using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            string id = UserManager.GetUserId(User);
            var projects = Context.Project.Where(p => p.UserId == id).ToList();
            
            ProjectViewModel project = new ProjectViewModel();
            foreach(var p in projects)
            {
                Console.WriteLine("test");
                project.Projectname.Add(p.Name);
                project.ProjectIds.Add(p.ProjectId);
            }
            Console.WriteLine(project.Projectname.Count());
            ViewData["RequestScheme"] = Request.Scheme;
            return View(project);
        }

        public IActionResult Show([FromQuery] int projectId)
        {
            
            return Redirect("http://localhost:4200/");
        }

        public IActionResult Create()
        {
            ViewData["RequestScheme"] = Request.Scheme;
            return View();
        }

        /**
         * Projects that the User has permission to join
         * because he got invited into these projects
         */
        public IActionResult Shared()
        {
            var sharedProjects = Context.ProjectParticipator
                .Where(u => u.UserId == UserManager.GetUserId(User))
                .Join(
                    Context.Project,
                    participator => participator.ProjectId,
                    project => project.ProjectId,
                    (participator, project) => new ProjectSharedViewModel
                    {
                       ProjectId = project.ProjectId,
                       Projectname = project.Name
                    }
                ).ToList();
            
            ViewData["RequestScheme"] = Request.Scheme;
            
            
            return View(sharedProjects);
        }
        
        [Authorize]
        public IActionResult Store([FromForm] ProjectStoreViewModel projectval)
        {
            string id = UserManager.GetUserId(User);
            
            ProjectModel project = new ProjectModel
            {
                UserId = id,
                Name = projectval.ProjectName
            };
            
            Context.Project.Add(project);
            Context.SaveChanges();
            
            foreach (var userId in projectval.UserIds)
            {
                ProjectParticipatorModel participator = new ProjectParticipatorModel
                {
                    ProjectId = project.ProjectId,
                    UserId = userId
                };
                Context.ProjectParticipator.Add(participator);
            }
            
            Context.SaveChanges();
            return RedirectToAction("Create",new{projectCreated = true});
        }

        [Authorize]
        public IActionResult Destroy([FromQuery]int projectId)
        {
            ProjectModel project = Context.Project.Find(projectId);
            Context.Project.Remove(project);
            Context.SaveChanges();
            Console.WriteLine("destroyed");
            return Ok();
        }

      //  [Authorize]
     //   [HttpGet]
        public IActionResult GetProjects([FromQuery] string userId)
        {
            string id = UserManager.GetUserId(User);

            var projects = Context.Project
                .Where(u => u.UserId == id).ToList();


            if (projects is null) return StatusCode(StatusCodes.Status404NotFound);

            List<ProjectGetViewModel> projectsvm = new List<ProjectGetViewModel>();

            foreach (var project in projects)
            {
                projectsvm.Add(new ProjectGetViewModel
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.Name
                });
            } 
            
            return Ok(projectsvm);
        }
    }
}
