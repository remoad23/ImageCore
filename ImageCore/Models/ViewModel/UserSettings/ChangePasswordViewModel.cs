using System.ComponentModel.DataAnnotations;

namespace ImageCore.ViewModel.UserSettings
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Passwort benötigt")]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Passwort benötigt")]
        [StringLength(100, ErrorMessage = "Das Passwort muss mindestens 8 Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "Passwort benötigt")]
        [StringLength(100, ErrorMessage = "Das Passwort muss mindestens 8 Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwort und Passwort bestätigen stimmen nicht überein.")]
        public string NewPasswordRepeat { get; set; }
    }
}