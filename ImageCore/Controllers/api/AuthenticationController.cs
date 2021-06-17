using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageCore.Controllers.api
{
    /*
     * responsible for the authentication of a project
     */
    public class AuthenticationController : Controller
    {
        
        [HttpGet]
        [Route("Api")]
        [Authorize(Policy = "IsProjectParticipator")]
        public IActionResult ValidateToken()
        {
            return Ok();
        }
    }
}