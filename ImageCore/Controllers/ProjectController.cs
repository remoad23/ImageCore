using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private RoleManager<IdentityRole> RoleManager;
        
        public ProjectController(
            ContextDb context,
            UserManager<UserModel> userManager,
            IProjectAuth projectAuth,
            RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
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
                string roleIdP = Context.Roles.Where(r => r.Name.Equals("ProjectEditor")).FirstOrDefault().Id;
                Console.WriteLine("roleID:    "+roleIdP);
                
                foreach (var userId in projectval.UserIds)
                {
                    ProjectParticipatorModel participator = new ProjectParticipatorModel
                    {
                        ProjectParticipatorId = Guid.NewGuid().ToString(),
                        ProjectId = project.ProjectId,
                        
                        UserId = userId
                    };

                    Context.ProjectParticipator.Add(participator);
                    Context.SaveChanges();
                    
                    var roleclaimP = new RoleClaim();
                    var userclaimP = new IdentityUserClaim<string>();
                    
                    Console.WriteLine("userId     "+userId);
                    Console.WriteLine("project.ProjectId     "+project.ProjectId);
                    Console.WriteLine("roleIdP     "+roleIdP);
                    
                    roleclaimP.ClaimType = ClaimTypes.Role;
                    roleclaimP.ClaimValue = userId;
                    roleclaimP.ClaimValue2 = project.ProjectId;
                    roleclaimP.RoleId = roleIdP;
                    Context.RoleClaims.Add(roleclaimP);
                    Context.SaveChanges();
                    
                    userclaimP.ClaimType = ClaimTypes.NameIdentifier;
                    userclaimP.ClaimValue = project.ProjectId;
                    userclaimP.UserId = userId;
                    Context.UserClaims.Add(userclaimP);
                    Context.SaveChanges();
                    
                }
            }

            string roleId = Context.Roles.Where(r => r.Name.Equals("ProjectOwner")).FirstOrDefault().Id;

            var roleclaim = new RoleClaim();
            var userclaim = new IdentityUserClaim<string>();
            
            roleclaim.ClaimType = ClaimTypes.Role;
            roleclaim.ClaimValue = id;
            roleclaim.ClaimValue2 = project.ProjectId;
            roleclaim.RoleId = roleId;

            userclaim.ClaimType = ClaimTypes.NameIdentifier;
            userclaim.ClaimValue = project.ProjectId;
            userclaim.UserId = id;
            
            Context.UserClaims.Add(userclaim);
            Context.RoleClaims.Add(roleclaim); 
            
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

        [Authorize(Roles="User,Admin")]
        public IActionResult Destroy([FromQuery]string projectId)
        {
            ProjectModel project = Context.Project.Find(projectId);
            Context.Project.Remove(project);
            Context.SaveChanges();
            return Ok();
        }
        
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
                if (Context.ProjectParticipator
                        .Where(pp =>
                            pp.UserId.Equals(userId) && pp.ProjectId.Equals(project.ProjectId)).FirstOrDefault() != null) continue;
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
