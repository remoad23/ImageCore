using System;
using System.IO;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Models.ViewModel;
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
        [HttpPost]
        [Route("uploadimagelayer")]
        public async Task<IActionResult> UploadImageLayer([FromForm]FileUploadViewModel upload)
        {

            Console.WriteLine("sdgsdgsgd");
            bool auth = ProjectAuth.VerifyToken(HttpContext, ContextDb);

            ImageModel model = new ImageModel()
            {
                FileName = "",
                Path = "",
                ProjectId = upload.ProjectId
            };

            ContextDb.Add(model);
            ContextDb.SaveChanges();
          
              if (upload.File.Length > 0)
              {

                  var filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                  using (var stream = System.IO.File.Create($"{filePath}/Imagelayers/{model.ImageId}"+".jpg"))
                  {
                      await upload.File.CopyToAsync(stream);
                  }
              }


            return auth ? Ok(model.ImageId) : Unauthorized();
        }

        [AllowAnonymous]
        [Route("getimagelayer")]
        public async Task< IActionResult> GetImageLayer([FromQuery] string fileId)
        {
            bool auth = ProjectAuth.VerifyToken(HttpContext, ContextDb);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagelayers/");
            var imageFileStream = System.IO.File.OpenRead(path+ fileId +".jpg");

            byte[] data = null;

                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFileStream.CopyToAsync(memoryStream);

                        data = memoryStream.ToArray();
                    }
                
            
            return Ok(data);
            //  return auth ? File(imageFileStream, "image/jpg") : Unauthorized();
        }
    }
}