using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ImageCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ImageCore.Requirements
{
    public class IsProjectParticipatorRequirement : AuthorizationHandler<IsProjectParticipatorRequirement>,IAuthorizationRequirement
    {
        private ContextDb Context;
        private UserManager<UserModel> UserManager;
        private RoleManager<IdentityRole> RoleManager;

        public IsProjectParticipatorRequirement()
        {
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsProjectParticipatorRequirement requirement)
        {
            Console.WriteLine("Test!");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Application for the Imagecore Imageprocessing App");

            var httpcontext = context.Resource as HttpContext;

            UserManager = httpcontext.RequestServices.GetRequiredService<UserManager<UserModel>>();
            RoleManager = httpcontext.RequestServices.GetRequiredService<RoleManager<IdentityRole>>();
            Context = httpcontext.RequestServices.GetRequiredService<ContextDb>();

            httpcontext.RequestServices.GetService(typeof(RoleManager<IdentityRole>));
            tokenHandler.ValidateToken(httpcontext.Request.Headers["Authorization"], new TokenValidationParameters
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

            string adminRoleId = RoleManager.GetRoleIdAsync(new IdentityRole("Admin")).Result;

            bool isAdmin = (role.Equals("Admin")
                            && Context.RoleClaims.Where(r =>
                                r.ClaimType.Equals(ClaimTypes.Role) && r.RoleId.Equals(adminRoleId) &&
                                r.ClaimValue.Equals(id) && r.ClaimValue2.Equals(project)).FirstOrDefault() != null
                            && UserManager.IsInRoleAsync(user, "Admin").Result);

            bool hasRole =
                Context.RoleClaims.Where(c =>
                        c.ClaimType.Equals(ClaimTypes.Role) && c.ClaimValue.Equals(role) &&
                        c.ClaimValue2.Equals(project))
                    .FirstOrDefault() != null;
            bool hasUser =
                Context.UserClaims
                    .Where(c => c.ClaimType.Equals(ClaimTypes.NameIdentifier) && c.ClaimValue.Equals(id))
                    .FirstOrDefault() != null;
            //    bool hasProject =
            //       Context.UserClaims
            //          .Where(c => c.ClaimType.Equals("Project") && c.ClaimValue.Equals(project))
            //         .FirstOrDefault() != null;

            context.Fail();

            /*
            Console.WriteLine("going here");
            if ((hasRole && hasUser) || isAdmin)
            {
                Console.WriteLine("PASSED");
                Console.WriteLine("hasrole:   " + hasRole);
                Console.WriteLine("isAdmin:   " + isAdmin);
                Console.WriteLine("hasUser:   " + hasUser);
                context.Succeed(requirement);
            }
            else
            {
                Console.WriteLine("FAILED");
                context.Fail();
            } */

            return Task.CompletedTask;
        }
    }
}