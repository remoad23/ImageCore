using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace ImageCore.Requirements
{
    public class IsProjectParticipatorRequirement: AuthorizationHandler<IsProjectParticipatorRequirement>, IAuthorizationRequirement
    {


        public IsProjectParticipatorRequirement()
        {

        }
        
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsProjectParticipatorRequirement requirement)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Application for the Imagecore Imageprocessing App");

            var httpcontext = context.Resource as HttpContext;

            try
            {
                tokenHandler.ValidateToken(httpcontext.Request.Headers["Authorization"], new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var name = jwtToken.Claims.First(x => x.Type == "User").Value;

                // return account id from JWT token if validation successful
                context.Succeed(requirement);
            }
            catch(Exception e)
            {
                context.Fail();
                httpcontext.Response.OnStarting(async () =>
                {
                    httpcontext.Response.StatusCode = 401;
                });
            }

            return Task.CompletedTask;
        }
    }
}