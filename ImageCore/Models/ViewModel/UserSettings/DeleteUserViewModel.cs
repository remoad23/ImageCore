using System.ComponentModel.DataAnnotations;

namespace ImageCore.ViewModel.UserSettings
{
    public class DeleteUserViewModel
    {
        [Required(ErrorMessage = "Passwort benötigt")]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+]*$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}