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
        
        public ProjectParticipatorController(ContextDb context,UserManager<UserModel> userManager)
        {
            Context = context;
            UserManager = userManager;
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
                ProjectId = projectId,
                UserId = userId
            };
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