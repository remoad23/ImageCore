using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "userName")]
        public string userName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Das Passwort muss mindestens 8 Zeichen lang sein.", MinimumLength = 8)]
   //     [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@@#\$%\^&\*])")]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string password { get; set; }
    }
}
