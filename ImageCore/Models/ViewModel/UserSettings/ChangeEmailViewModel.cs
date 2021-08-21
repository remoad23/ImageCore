using System.ComponentModel.DataAnnotations;

namespace ImageCore.ViewModel.UserSettings
{
    public class ChangeEmailViewModel
    {
        [Required(ErrorMessage = "E-Mail Adresse benötigt")]
        [EmailAddress(ErrorMessage = "E-Mail Adresse nicht valide")]
        public string CurrentEmail { get; set; }

        [Required(ErrorMessage = "E-Mail Adresse benötigt")]
        [EmailAddress(ErrorMessage = "E-Mail Adresse nicht valide")]
        public string NewEmail { get; set; }
        
        [Required(ErrorMessage = "E-Mail Adresse benötigt")]
        [EmailAddress(ErrorMessage = "E-Mail Adresse nicht valide")]
        [Compare("NewEmail", ErrorMessage = "Email's stimmen nicht überein")]
        public string NewEmailRepeat { get; set; }
    }
}