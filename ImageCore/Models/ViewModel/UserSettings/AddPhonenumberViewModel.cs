using System.ComponentModel.DataAnnotations;

namespace ImageCore.ViewModel.UserSettings
{
    public class AddPhonenumberViewModel
    {
        [Required] 
        [Phone] 
        public string PhoneNumber { get; set; }

        [Required] 
        [Phone] 
        [Compare("PhoneNumber", ErrorMessage = "Nummer stimmen nicht überein")]
        public string PhoneNumberRepeat { get; set; }
        
    }
}