using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ImageCore.Services
{
    public interface IImageLoader
    {

        /**
         * Get an Image as byte array
         */
        public byte[] GetImage(string fileName, string folderName);

        public string UploadImage(IFormFile file, string filename, bool randomize = false);

        public byte[] GetProfileImage(ClaimsPrincipal claim);

        public byte[] GetProfileImage(string image);
    }
}