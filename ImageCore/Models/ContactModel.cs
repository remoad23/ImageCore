using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models
{
    public class ContactModel
    {
        [Key]
        public string ContactId { get; private set; }
        
        [ForeignKey("UserId")]
        [Required]
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        [Required]
        public string ContactUserId { get; set; }

        public bool RequestValidated { get; set; }
        
        [ForeignKey("UserId")]
        public UserModel User { get; set; }
        [ForeignKey("ContactUserId")]
        public UserModel ContactUser { get; set; }

    }
}
