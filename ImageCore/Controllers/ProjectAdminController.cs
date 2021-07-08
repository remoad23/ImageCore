using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel;
using ImageCore.Models.ViewModel.User;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Controllers
{
    public class ProjectAdminController : Controller
    {
        
        private ContextDb Dbcontext;
        private UserManager<UserModel> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        private IMailSend MailSend;
        
        public ProjectAdminController(ContextDb dbContext,
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
            var ProjectList = (dynamic)null;
            ViewData["RequestScheme"] = Request.Scheme;
            int pag = (Dbcontext.Users.Count() / 10);
            ViewData["paginationMax"] = (pagination + 5) > pag ? pag : pagination + 5;
            ViewData["paginationMin"] = (pagination - 5) < 0 ? 0 : pagination - 5;
            
            if (pagination is null)
            {
                if (query is not null)
                {
                    ProjectList = Dbcontext.Project
                        .Where(u => u.Name.Contains(query) || u.Name.ToLower().Contains(query))
                        .Join(
                            Dbcontext.Users,
                            model => model.UserId,
                            user => user.Id,
                            (project, user) => new ProjectListViewModel
                            {
                                ProjectId = project.ProjectId,
                                Name = project.Name,
                                Owner = user.UserName,
                            }
                        )
                        .OrderBy(u => u.Name)
                        .Take(10)
                        .ToList();

                    foreach (ProjectListViewModel project in ProjectList)
                    {
                        project.ParticipatorCount = Dbcontext.ProjectParticipator
                            .Where(pp => pp.ProjectId == project.ProjectId)
                            .ToList()
                            .Count;
                    }
                }
                else
                {
                    ProjectList = Dbcontext.Project
                        .Join(
                            Dbcontext.Users,
                            model => model.UserId,
                            user => user.Id,
                            (project, user) => new ProjectListViewModel
                            {
                                ProjectId = project.ProjectId,
                                Name = project.Name,
                                Owner = user.UserName,
                            }
                        )
                        .OrderBy(u => u.Name)
                        .Take(10)
                        .ToList();
                    
                    foreach (ProjectListViewModel project in ProjectList)
                    {
                        project.ParticipatorCount = Dbcontext.ProjectParticipator
                            .Where(pp => pp.ProjectId == project.ProjectId)
                            .ToList()
                            .Count;
                    }
                }
               
            }
            else
            {
                if (query is not null)
                {
                    ProjectList = Dbcontext.Project
                        .Where(u => u.Name.Contains(query) || u.Name.ToLower().Contains(query))
                        .Join(
                            Dbcontext.Users,
                            model => model.UserId,
                            user => user.Id,
                            (project, user) => new ProjectListViewModel
                            {
                                ProjectId = project.ProjectId,
                                Name = project.Name,
                                Owner = user.UserName,
                            }
                        )
                        .OrderBy(u => u.Name)
                        .Skip((int)pagination * 10)
                        .Take(10)
                        .ToList();
                    
                    foreach (ProjectListViewModel project in ProjectList)
                    {
                        project.ParticipatorCount = Dbcontext.ProjectParticipator
                            .Where(pp => pp.ProjectId == project.ProjectId)
                            .ToList()
                            .Count;
                    }
                }
                else
                {
                    ProjectList = Dbcontext.Project
                        .Join(
                            Dbcontext.Users,
                            model => model.UserId,
                            user => user.Id,
                            (project, user) => new ProjectListViewModel
                            {
                                ProjectId = project.ProjectId,
                                Name = project.Name,
                                Owner = user.UserName,
                            }
                        )
                        .OrderBy(u => u.Name)
                        .Skip((int)pagination * 10)
                        .Take(10)
                        .ToList();
                    
                    foreach (ProjectListViewModel project in ProjectList)
                    {
                        project.ParticipatorCount = Dbcontext.ProjectParticipator
                            .Where(pp => pp.ProjectId == project.ProjectId)
                            .ToList()
                            .Count;
                    }
                }
            }

            var list = new List<ProjectListViewModel>();
            if (descend is not null)
            {
                if (descend.Equals("true")) list = ((List<ProjectListViewModel>) ProjectList).OrderByDescending(u => u.Name).ToList();
                return View("~/Views/Project/ProjectAdmin/Index.cshtml",list);
            }
            else
            {
                return View("~/Views/Project/ProjectAdmin/Index.cshtml",ProjectList);
            }
            
        }
        #nullable disable
    }
}