using System;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Services;
using ImageCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers.api
{
    /*
     * responsible for the authentication of a project
     */
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        private IProjectAuth ProjectAuth;
        private ContextDb ContextDb;
        public AuthenticationController(IProjectAuth projectAuth,ContextDb contextDb)
        {
            ContextDb = contextDb;
            ProjectAuth = projectAuth;
        }
        
        [AllowAnonymous]
        [Route("AuthApi")]
        public IActionResult ValidateToken()
        {
            bool auth = ProjectAuth.VerifyToken(HttpContext, ContextDb);
            Console.WriteLine(auth);
            return auth ? Ok() : Unauthorized();
        }
    }
}