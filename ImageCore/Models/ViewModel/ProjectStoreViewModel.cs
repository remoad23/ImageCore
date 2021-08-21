using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageCore.Models.ViewModel
{
    public class ProjectStoreViewModel
    {
        [Required(ErrorMessage = "Projektname benötigt")]
        [StringLength(40, ErrorMessage = "Projektname muss mindestens 8  Zeichen lang sein.", MinimumLength = 8)]
        [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage = "Sonderzeichen nicht erlaubt")]
        public string ProjectName { get; set; }
        
        [Required(ErrorMessage = "Höhe benötigt")]
        [RegularExpression("^[0-9]*$",ErrorMessage = "Zahlen nur erlaubt")]
        public int Height { get; set; }
        
        [Required(ErrorMessage = "Höhe benötigt")]
        [RegularExpression("^[0-9]*$",ErrorMessage = "Zahlen nur erlaubt")]
        public int Width { get; set; }
        public List<string> UserIds { get; set; }

    }
}