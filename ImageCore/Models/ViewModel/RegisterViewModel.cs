using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.ViewModel
{
    public class RegisterViewModel
    {
        
        [Required(ErrorMessage = "E-Mail Adresse benötigt")]
        [EmailAddress(ErrorMessage = "E-Mail Adresse nicht valide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Benutzername benötigt")]
        [StringLength(30, ErrorMessage = "Benutzername muss mindestens 8  Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage = "Sonderzeichen nicht erlaubt")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Passwort benötigt")]
        [StringLength(100, ErrorMessage = "Das Passwort muss mindestens 8 Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Passwort benötigt")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [Compare("Password", ErrorMessage = "Passwörter stimmen nicht überein.")]
        public string PasswordRepeat{  get; set; }
        
        
    }
}
