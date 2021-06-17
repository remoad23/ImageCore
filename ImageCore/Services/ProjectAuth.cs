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
        
        public string CreateToken(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim("User", user.Id)
            };
            
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Application for the Imagecore Imageprocessing App"));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            
            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}