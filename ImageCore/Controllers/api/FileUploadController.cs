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
using System.Text.Json;
using System.Linq;

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


        [AllowAnonymous]
        [HttpPost]
        [Route("saveproject")]
        public async Task<IActionResult> SaveProject([FromForm] string[] projectData,[FromForm] Guid[] imageLayerId)
        {
            bool auth = ProjectAuth.VerifyToken(HttpContext, ContextDb);
            //Console.WriteLine(projectData.Length);
            //Console.WriteLine(imageLayerId.Length);
            var layerIDs = new Guid[imageLayerId.Length];
            
            for (int i = 0; i < projectData.Length; i++)
            {
                var data = JsonSerializer.Deserialize<ImageLayerModel>(projectData[i]);
                Console.WriteLine(imageLayerId[i]);
                Console.WriteLine(imageLayerId[i] != Guid.Empty);
                if (imageLayerId[i] != Guid.Empty)
                {
                    var imgLayer = ContextDb.ImageLayer.Find(imageLayerId[i]);
                    if (imgLayer.OriginalImageMat != data.OriginalImageMat)
                    {
                        imgLayer.OriginalImageMat = data.OriginalImageMat;
                    }
                    if (imgLayer.ProcessedImageMat != data.ProcessedImageMat)
                    {
                        imgLayer.ProcessedImageMat = data.ProcessedImageMat;
                    }
                    if (imgLayer.Width != data.Width)
                    {
                        imgLayer.Width = data.Width;
                    }
                    if (imgLayer.Height != data.Height)
                    {
                        imgLayer.Height = data.Height;
                    }
                    if (imgLayer.Masked != data.Masked)
                    {
                        imgLayer.Masked = data.Masked;
                    }
                    if (imgLayer.Rotation != data.Rotation)
                    {
                        imgLayer.Rotation = data.Rotation;
                    }
                    if (imgLayer.LayerColor != data.LayerColor)
                    {
                        imgLayer.LayerColor = data.LayerColor;
                    }
                    if (imgLayer.FontSize != data.FontSize)
                    {
                        imgLayer.FontSize = data.FontSize;
                    }
                    if (imgLayer.FontStrength != data.FontStrength)
                    {
                        imgLayer.FontStrength = data.FontStrength;
                    }
                    if (imgLayer.Text != data.Text)
                    {
                        imgLayer.Text = data.Text;
                    }
                    if (imgLayer.FilterId != data.FilterId)
                    {
                        imgLayer.FilterId = data.FilterId;
                    }
                    if (imgLayer.Name != data.Name)
                    {
                        imgLayer.Name = data.Name;
                    }
                    if (imgLayer.MaskMat != data.MaskMat)
                    {
                        imgLayer.MaskMat = data.MaskMat;
                    }
                    if (imgLayer.X != data.X)
                    {
                        imgLayer.X = data.X;
                    }
                    if (imgLayer.Y != data.Y)
                    {
                        imgLayer.Y = data.Y;
                    }
                    if (imgLayer.Z != data.Z)
                    {
                        imgLayer.Z = data.Z;
                    }
                    if (imgLayer.Opacity != data.Opacity)
                    {
                        imgLayer.Opacity = data.Opacity;
                    }
                    if (imgLayer.Visible != data.Visible)
                    {
                        imgLayer.Visible = data.Visible;
                    }
                    if (imgLayer.LayerType != data.LayerType)
                    {
                        imgLayer.LayerType = data.LayerType;
                    }

                }
                else
                {
                    ContextDb.Add(data);
                }

                layerIDs[i] = (data.ImageLayerId == Guid.Empty) ? imageLayerId[i] : data.ImageLayerId;
            }
            
            ContextDb.SaveChanges();

            


            return auth ? Ok(layerIDs) : Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("savelayer")]
        public async Task<IActionResult> SaveLayer([FromForm] string projectData, [FromForm] Guid imageLayerId)
        {
            bool auth = ProjectAuth.VerifyToken(HttpContext, ContextDb);
            Console.WriteLine("HI");
            var data = JsonSerializer.Deserialize<ImageLayerModel>(projectData);
            Console.WriteLine(imageLayerId);
            Console.WriteLine(imageLayerId != Guid.Empty);
            if (imageLayerId != Guid.Empty)
            {
                var imgLayer = ContextDb.ImageLayer.Find(imageLayerId);
                if (imgLayer.OriginalImageMat != data.OriginalImageMat)
                {
                    imgLayer.OriginalImageMat = data.OriginalImageMat;
                }
                if (imgLayer.ProcessedImageMat != data.ProcessedImageMat)
                {
                    imgLayer.ProcessedImageMat = data.ProcessedImageMat;
                }
                if (imgLayer.Width != data.Width)
                {
                    imgLayer.Width = data.Width;
                }
                if (imgLayer.Height != data.Height)
                {
                    imgLayer.Height = data.Height;
                }
                if (imgLayer.Masked != data.Masked)
                {
                    imgLayer.Masked = data.Masked;
                }
                if (imgLayer.Rotation != data.Rotation)
                {
                    imgLayer.Rotation = data.Rotation;
                }
                if (imgLayer.LayerColor != data.LayerColor)
                {
                    imgLayer.LayerColor = data.LayerColor;
                }
                if (imgLayer.FontSize != data.FontSize)
                {
                    imgLayer.FontSize = data.FontSize;
                }
                if (imgLayer.FontStrength != data.FontStrength)
                {
                    imgLayer.FontStrength = data.FontStrength;
                }
                if (imgLayer.Text != data.Text)
                {
                    imgLayer.Text = data.Text;
                }
                if (imgLayer.FilterId != data.FilterId)
                {
                    imgLayer.FilterId = data.FilterId;
                }
                if (imgLayer.Name != data.Name)
                {
                    imgLayer.Name = data.Name;
                }
                if (imgLayer.MaskMat != data.MaskMat)
                {
                    imgLayer.MaskMat = data.MaskMat;
                }
                if (imgLayer.X != data.X)
                {
                    imgLayer.X = data.X;
                }
                if (imgLayer.Y != data.Y)
                {
                    imgLayer.Y = data.Y;
                }
                if (imgLayer.Z != data.Z)
                {
                    imgLayer.Z = data.Z;
                }
                if (imgLayer.Opacity != data.Opacity)
                {
                    imgLayer.Opacity = data.Opacity;
                }
                if (imgLayer.Visible != data.Visible)
                {
                    imgLayer.Visible = data.Visible;
                }
                if (imgLayer.LayerType != data.LayerType)
                {
                    imgLayer.LayerType = data.LayerType;
                }

            }
            else
            {
                ContextDb.Add(data);
            }

            var layerID= (data.ImageLayerId == Guid.Empty) ? imageLayerId : data.ImageLayerId;

            ContextDb.SaveChanges();




            return auth ? Ok(layerID) : Unauthorized();
        }

        [AllowAnonymous]
        [Route("getproject")]
        public async Task<IActionResult> GetProject([FromQuery] string projectId)
        {
            bool auth = ProjectAuth.VerifyToken(HttpContext, ContextDb);

            var imageLayers = ContextDb.ImageLayer
                .Where(l => l.ProjectId == projectId)
                .OrderBy(l => l.Z).ToList();

            return Ok(imageLayers);
            //  return auth ? File(imageFileStream, "image/jpg") : Unauthorized();
        }
    }
}