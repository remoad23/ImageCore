using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ImageCore.Controllers
{
    public class ProjectParticipatorController : Controller
    {
        private ContextDb Context;
        private UserManager<UserModel> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        
        public ProjectParticipatorController(ContextDb context,UserManager<UserModel> userManager,RoleManager<IdentityRole> roleManager)
        {
            Context = context;
            UserManager = userManager;
            RoleManager = roleManager;
        }
        
        
        [HttpGet]
        public IActionResult Show(string projectParticipatorId)
        {
            ProjectParticipatorModel participatorModel = Context.ProjectParticipator.Find(projectParticipatorId);
            // return participatorModel;  <-- hier später json
            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        
        /**
         * Invite a new User to a project
         */
        [Authorize(Roles="Admin,User")]
        [HttpPost]
        public IActionResult Store(string userId,string projectId)
        {
            ProjectParticipatorModel participator = new ProjectParticipatorModel
            {
                ProjectParticipatorId = Guid.NewGuid().ToString(),
                ProjectId = projectId,
                UserId = userId
            };


            string roleId = Context.Roles.Where(r => r.Name.Equals("ProjectEditor")).FirstOrDefault().Id;

            var roleclaim = new RoleClaim();
            var userclaim = new IdentityUserClaim<string>();
            
            roleclaim.ClaimType = ClaimTypes.Role;
            roleclaim.ClaimValue = userId;
            roleclaim.ClaimValue2 = projectId;
            roleclaim.RoleId = roleId;

            userclaim.ClaimType = ClaimTypes.NameIdentifier;
            userclaim.ClaimValue = projectId;
            userclaim.UserId = userId;
            
            Context.UserClaims.Add(userclaim);
            Context.RoleClaims.Add(roleclaim); 
            Context.ProjectParticipator.Add(participator);
            Context.SaveChanges(); 
            return Ok();
        }

        /**
         * get rid of user inside a Project
         */
        [Authorize]
        [Authorize(Roles="Admin,User")]
        public IActionResult Destroy(string projectParticipatorId)
        {
            ProjectParticipatorModel participatorModel = Context.ProjectParticipator.Find(projectParticipatorId);
            Context.ProjectParticipator.Remove(participatorModel);
            Context.SaveChanges();
            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}