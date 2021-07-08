using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.ViewModel.UserSettings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Drawing;
using ImageCore.Services;

namespace ImageCore.Controllers.UserSettings
{
    public class ChangeUserImageController : Controller
    {
        
        private readonly UserManager<UserModel> Usermanager;
        private readonly ContextDb ContextDb;
        private readonly IImageLoader ImageLoader;

        public ChangeUserImageController(UserManager<UserModel>  userManager,
            ContextDb contextDb,
            IImageLoader imageLoader)
        {
            this.Usermanager = userManager;
            this.ContextDb = contextDb;
            this.ImageLoader = imageLoader;

        }
        
        // GET
        public IActionResult Index()
        {
            return View("~/Views/UserSettings/ChangeUserImage/Index.cshtml");
        }

        public async Task<IActionResult> Store(ChangeProfileImageViewModel model)
        {
            string user = Usermanager.GetUserId(User);


            string imageName = ImageLoader.UploadImage(model.File, "", true);
            ContextDb.Users.Find(user).image = imageName;
            ContextDb.SaveChanges();
            TempData["Message"] = "Bild erfolgreich geändert";
            return RedirectToAction("Index");
        }
        
    }
}
