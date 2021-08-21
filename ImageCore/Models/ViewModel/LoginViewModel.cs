using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Benutzername benötigt")]
        [Display(Name = "userName")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Passwort benötigt")]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string password { get; set; }
    }
}
