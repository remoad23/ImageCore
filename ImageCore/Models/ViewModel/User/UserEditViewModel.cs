using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ImageCore.Models.ViewModel.User
{
    public class UserEditViewModel
    {
        
        [MaxLength(40,ErrorMessage = "Bitte wähle ein Benutzernamen mit weniger als 40 Zeichen")]
        [MinLength(10,ErrorMessage = "Bitte wähle ein Benutzernamen mit mehr als 10 Zeichen")]
        public string Username { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Role { get; set; }
        
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [FileExtensions(Extensions = (".png,.jpg,.jpeg"), ErrorMessage = "Falsche Dateiformat.Bitte Laden ein Bild mit dem Format png,jpg oder jpeg hoch.")]
        public IFormFile File { get; set; }
        
        public string UserImage { get; set; }
    }
}