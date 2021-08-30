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
using Microsoft.Extensions.DependencyInjection;

namespace ImageCore.Services
{
    public class ProjectAuth : IProjectAuth
    {
        private UserManager<UserModel> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        private ContextDb Context;
        private IConfiguration Configuration;
        
        public ProjectAuth(ContextDb context,UserManager<UserModel> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            Context = context;
            UserManager = userManager;
            Configuration = configuration;
            RoleManager = roleManager;
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

        public bool VerifyToken(HttpContext context,ContextDb Context)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Application for the Imagecore Imageprocessing App");

            if (context.Request.Headers["Authorization"].Count == 0 ||
                context.Request.Headers["Authorization"].Equals("")) return false;

            tokenHandler.ValidateToken(context.Request.Headers["Authorization"], new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken) validatedToken;
            var project = jwtToken.Claims.First(x => x.Type == "Project").Value;
            var role = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            var id = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            UserModel user = Context.Users.Find(id);

            var adminRoleId = Context.Roles.Where(r => r.Name.Equals("Admin")).FirstOrDefault();
            
            string userRoleId = Context.Roles.Where(r => r.Name.Equals("ProjectEditor")).FirstOrDefault().Id;
            
            string userOwnerId = Context.Roles.Where(r => r.Name.Equals("ProjectOwner")).FirstOrDefault().Id;

            bool isAdmin = (role.Equals("Admin") && UserManager.IsInRoleAsync(user, "Admin").Result && adminRoleId != null);

            bool isEditorRole =
                Context.RoleClaims.Where(c =>
                        c.ClaimType.Equals(ClaimTypes.Role) 
                        && c.RoleId.Equals(userRoleId) 
                        && c.ClaimValue.Equals(id) 
                        && c.ClaimValue2.Equals(project))
                    .FirstOrDefault() != null;
            
            bool hasUser =
                Context.UserClaims
                    .Where(c =>
                        c.ClaimType.Equals(ClaimTypes.NameIdentifier) 
                        && c.ClaimValue.Equals(project) 
                        && c.UserId.Equals(id))
                    .FirstOrDefault() != null;
            
            bool isOwnerRole = Context.RoleClaims.Where(c =>
                    c.ClaimType.Equals(ClaimTypes.Role) 
                    && c.RoleId.Equals(userOwnerId) 
                    && c.ClaimValue.Equals(id) 
                    && c.ClaimValue2.Equals(project))
                .FirstOrDefault() != null;
            
           /*
            
            Console.WriteLine("isEditorRole   " + isEditorRole);
            Console.WriteLine("hasUser   " + hasUser);
            Console.WriteLine("isAdmin   " + isAdmin);
            Console.WriteLine("isOwnerRole   " + isOwnerRole);
            
            */
            
            if ((isEditorRole && hasUser) || isAdmin || isOwnerRole)
            {
                Console.WriteLine("PASSED");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}