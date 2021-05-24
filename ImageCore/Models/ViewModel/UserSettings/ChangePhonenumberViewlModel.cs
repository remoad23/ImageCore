using System.ComponentModel.DataAnnotations;

namespace ImageCore.ViewModel.UserSettings
{
    public class ChangePhonenumberViewlModel
    {
        [Required]
        [Phone]
        public string CurrentPhoneNumber { get; set; }

        [Required]
        [Phone]
        public string NewPhoneNumber { get; set; }
        
        [Required]
        [Phone]
        [Compare("NewPhoneNumber", ErrorMessage = "Nummer stimmen nicht überein")]
        public string NewPhoneNumberRepeat { get; set; }
    }
}