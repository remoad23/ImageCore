using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ImageCore.Services
{
    public class ProjectAuth : IProjectAuth
    {
        private UserManager<UserModel> UserManager;
        private IConfiguration Configuration;
        
        public ProjectAuth(UserManager<UserModel> userManager,IConfiguration configuration)
        {
            UserManager = userManager;
            Configuration = configuration;
        }

        public string CreateToken(UserModel user, string projectId,ContextDb context)
        {
            string role = "";
            bool isAdmin = UserManager.IsInRoleAsync(user, "Admin").Result;
            if (!isAdmin)
            {
                var participator = context.ProjectParticipator
                    .Where(pp => pp.ProjectId.Equals(projectId) && pp.UserId.Equals(user.Id)).ToList();

                if (participator.Count == 0)
                {
                    var owner = context.Project
                        .Where(p => p.ProjectId.Equals(projectId) && p.UserId.Equals(user.Id))
                        .ToList();

                    if (owner.Count == 0)
                    {
                        
                    }
                    else
                    {
                        role = "ProjectOwner";
                    }
                }
                else
                {
                    role = "ProjectEditor";
                }
            }
            else
            {
                role = "Admin";
            }


            var claims = new List<Claim>
            {
                new Claim("Project",projectId),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Application for the Imagecore Imageprocessing App"));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            
            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(600), signingCredentials: credentials
                );
            
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}