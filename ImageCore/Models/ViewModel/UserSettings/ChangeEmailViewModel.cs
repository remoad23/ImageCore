using System.ComponentModel.DataAnnotations;

namespace ImageCore.ViewModel.UserSettings
{
    public class ChangeEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string CurrentEmail { get; set; }

        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
        
        [Required]
        [EmailAddress]
        [Compare("NewEmail", ErrorMessage = "Passwörter stimmen nicht überein")]
        public string NewEmailRepeat { get; set; }
    }
}