using System.ComponentModel.DataAnnotations;

namespace ImageCore.ViewModel.UserSettings
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        public string NewPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [Compare("NewPassword", ErrorMessage = "Passwort und Passwort bestätigen stimmen nicht überein.")]
        public string NewPasswordRepeat { get; set; }
    }
}