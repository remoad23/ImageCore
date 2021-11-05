using System;
using System.IO;
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
    public class FileUploadController : Controller
    {
        private IProjectAuth ProjectAuth;
        private ContextDb ContextDb;

        public FileUploadController(IProjectAuth projectAuth,ContextDb contextDb)
        {
            ContextDb = contextDb;
            ProjectAuth = projectAuth;
        }
        
        [AllowAnonymous]
        [Route("uploadimagelayer")]
        public async Task<IActionResult> UploadImageLayer([FromForm]IFormFile file,[FromQuery] string projectId)
        {
            Console.WriteLine("sdgsdgsgd");
            bool auth = ProjectAuth.VerifyToken(HttpContext, ContextDb);

          
              string imageId = "";
              if (file.Length > 0)
              {

                  var filePath = Path.GetTempFileName();

                  using (var stream = System.IO.File.Create(filePath + imageId))
                  {
                      await file.CopyToAsync(stream);
                  }
              }


            return auth ? Ok(imageId) : Unauthorized();
        }

        [AllowAnonymous]
        [Route("getimagelayer")]
        public IActionResult GetImageLayer(string fileName, [FromQuery] string fileId)
        {
            return null;
          //  return auth ? Ok() : Unauthorized();
        }
    }
}