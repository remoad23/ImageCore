using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.ViewModel
{
    public class RegisterViewModel
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Benutzername muss mindestens 8 und maximal Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string Username { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Das Passwort muss mindestens 8 Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [Compare("Password", ErrorMessage = "Passwort und Passwort bestätigen stimmen nicht überein.")]
        public string PasswordRepeat{  get; set; }
        
        
    }
}
