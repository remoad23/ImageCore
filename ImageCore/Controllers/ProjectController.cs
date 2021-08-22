using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel;
using ImageCore.Services;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectAuth ProjectAuth;
        private ContextDb Context;
        private UserManager<UserModel> UserManager;
        
        public ProjectController(
            ContextDb context,
            UserManager<UserModel> userManager,
            IProjectAuth projectAuth)
        {
            ProjectAuth = projectAuth;
            Context = context;
            UserManager = userManager;
        }
        
        [Authorize(Roles="User,Admin")]
        public async Task<IActionResult> Index()
        {
            string id = UserManager.GetUserId(User);
            var projects = Context.Project.Where(p => p.UserId == id).ToList();
            
            ProjectViewModel project = new ProjectViewModel();
            foreach(var p in projects)
            {
                project.Projectname.Add(p.Name);
                project.ProjectIds.Add(p.ProjectId);
            }
            ViewData["RequestScheme"] = Request.Scheme;
            return View(project);
        }

        [Authorize(Roles="User,Admin")]
        public IActionResult Show([FromQuery] string projectId)
        {

            UserModel user = UserManager.GetUserAsync(User).Result;
            var token = ProjectAuth.CreateToken(user,projectId,Context);

            return Redirect("http://localhost:4200/?token=" + token );
        }

        /*
        [Route("RedirectToProject")]
        [Authorize(Policy = "IsProjectParticipator")]
        public IActionResult RedirectToProject([FromQuery] string token)
        {
            return Redirect("http://localhost:4200/");
        } 
        */

        [Authorize(Roles="User,Admin")]
        public IActionResult Create()
        {
            ViewData["RequestScheme"] = Request.Scheme;
            return View();
        }

        
        
        /**
         * Projects that the User has permission to join
         * because he got invited into these projects
         */
        [Authorize(Roles="User,Admin")]
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
        
        
        [Authorize(Roles="User,Admin")]
        public IActionResult Store([FromForm] ProjectStoreViewModel projectval)
        {
            ViewData["RequestScheme"] = Request.Scheme;
            if (!ModelState.IsValid) return View("Create");
            string id = UserManager.GetUserId(User);
            
            ProjectModel project = new ProjectModel
            {
                ProjectId = Guid.NewGuid().ToString(),
                UserId = id,
                Name = projectval.ProjectName,
                Views = 0
            };

            Context.Project.Add(project);
            Context.SaveChanges();

            if (projectval.UserIds is not null)
            {
                foreach (var userId in projectval.UserIds)
                {
                    ProjectParticipatorModel participator = new ProjectParticipatorModel
                    {
                        ProjectId = project.ProjectId,
                        UserId = userId
                    };
                    Context.ProjectParticipator.Add(participator);
                }
            }
            
            Context.SaveChanges();
            return RedirectToAction("Create",new{projectCreated = true});
        }

        [Authorize(Roles="User,Admin")]
        public IActionResult QueryProjects([FromQuery] string query)
        {
            string id = UserManager.GetUserId(User);
            var projects = Context.Project
                .Where(p => p.UserId == id && p.Name.Contains(query))
                .ToList();
            
            ProjectViewModel project = new ProjectViewModel();
            foreach(var p in projects)
            {
                project.Projectname.Add(p.Name);
                project.ProjectIds.Add(p.ProjectId);
            }
            ViewData["RequestScheme"] = Request.Scheme;
         
            return View("~/Views/Project/Index.cshtml",project);
        }

        [Authorize(Roles="User,Admin")]
        public IActionResult QuerySharedProjects([FromQuery] string query)
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
                )
                .Where(p => p.Projectname.Contains(query))
                .ToList();
            
            ViewData["RequestScheme"] = Request.Scheme;
            
            return View("~/Views/Project/Shared.cshtml",sharedProjects);
        }

        [Authorize]
        [Authorize(Roles="User,Admin")]
        public IActionResult Destroy([FromQuery]int projectId)
        {
            ProjectModel project = Context.Project.Find(projectId);
            Context.Project.Remove(project);
            Context.SaveChanges();
            return Ok();
        }

      //  [Authorize]
     //   [HttpGet]
        [Authorize(Roles="User,Admin")]
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
