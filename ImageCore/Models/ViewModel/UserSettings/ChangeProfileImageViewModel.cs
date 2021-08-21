using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ImageCore.ViewModel.UserSettings
{
    public class ChangeProfileImageViewModel
    {
        [FileExtensions(Extensions = (".png,.jpg,.jpeg"), ErrorMessage = "Falsche Dateiformat.Bitte Laden ein Bild mit dem Format png,jpg oder jpeg hoch.")]
        public IFormFile File { get; set; }
    }
}