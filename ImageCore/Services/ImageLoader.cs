using System;
using System.Drawing;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Services
{
    /**
     * Used to upload or display any images
     */
    public class ImageLoader : IImageLoader
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<UserModel> Usermanager;
        private readonly ContextDb ContextDb; 

        public ImageLoader(IWebHostEnvironment hostEnvironment,
            UserManager<UserModel>  userManager,
            ContextDb contextDb)
        {
            this._hostEnvironment = hostEnvironment;
            this.Usermanager = userManager;
            this.ContextDb = contextDb;
        }

        public byte[] GetProfileImage(ClaimsPrincipal claim)
        {
            try
            {
                string user = Usermanager.GetUserId(claim);
                UserModel userm = ContextDb.Users.Find(user);
                return GetImage(userm.image, "profileimages");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
        
        public byte[] GetProfileImage(string image)
        {
            try
            {
                return GetImage(image, "profileimages");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public byte[] GetImage(string fileName,string folderName)
        {
            try
            {
                var filePath = Path.Combine(_hostEnvironment.WebRootPath + "//" +  folderName + "//" + fileName);
                if (!File.Exists(filePath)) return null;
                ImageConverter converter = new ImageConverter();
                Bitmap imageObj = new Bitmap(filePath);

                return (byte[])converter.ConvertTo(imageObj, typeof(byte[]));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public string UploadImage(IFormFile file,string filename,bool randomize = false)
        {
            try
            {
                var filePath = Path.Combine(
                    _hostEnvironment.WebRootPath + "/profileimages", 
                    randomize ? Path.GetRandomFileName() : filename );

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                    int slashIndex = stream.Name.LastIndexOf(@"\");
                    string fileName = stream.Name.Substring(slashIndex);
                    return fileName ;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}