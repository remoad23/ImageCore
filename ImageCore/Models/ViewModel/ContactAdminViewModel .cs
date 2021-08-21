using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models.ViewModel
{
    public class ContactAdminViewModel
    {
        [Required(ErrorMessage = "Betreff benötigt")]
        [StringLength(30, ErrorMessage = "Benutzername muss mindestens 8  Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage = "Sonderzeichen nicht erlaubt")]
        public string Topic { get; set; }
        [Required(ErrorMessage = "E-Mail Adresse benötigt")]
        [EmailAddress(ErrorMessage = "E-Mail Adresse nicht valide")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nachricht benötigt")]
        [StringLength(500, ErrorMessage = "Nachricht muss mindestens 8  Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage = "Sonderzeichen nicht erlaubt")]
        public string Message { get; set; }
    }
}
